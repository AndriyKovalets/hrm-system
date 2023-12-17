using Hrm.Application.Abstractions.Repositories;
using Hrm.Application.Abstractions.Services;
using Hrm.Domain.Entities;
using Hrm.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Services
{
    internal class VacationPlanService : IVacationPlanService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<User> _userRepository;
        private readonly ISettingsService _settingsService;
        private readonly IRepository<VacationPlan> _vacationPlanRepository;

        public VacationPlanService(
            IRepository<Department> departmentRepository,
            IRepository<User> userRepository,
            ISettingsService settingsService,
            IRepository<VacationPlan> vacationPlanRepository)
        {
            _departmentRepository = departmentRepository;
            _userRepository = userRepository;
            _settingsService = settingsService;
            _vacationPlanRepository = vacationPlanRepository;
        }

        public async Task CreateVacationPlanAsync()
        {
            var settings = await _settingsService.GetVacationPlanSettingsAsync();
            var employeeWithRateList = await GetEmployeeWithVacationRate();
            var minEmployeeNeedInDepartnemtList = await GetMinEmployeeNeedInDepartnemt();

            var plans = new List<List<User>>
            {
                GetPlan(employeeWithRateList)
            };

            for(int i=1; i<settings.N; i++)
            {
                plans.Add(GetPlan(employeeWithRateList));
            }

            var maxRate = employeeWithRateList.Sum(x => x.VacationRates.Max(y => y.Rate));

            plans = plans.OrderByDescending(x => GetFitnessFunction(x)).ToList();
            var alhpaPlan = plans.First();
            var maxFitnessFunction = 0;

            for (int i = 0; i < settings.Max; i++)
            {
                //search, attack
                for (int j = 0; j < settings.TMax; j++)
                {
                    foreach (var plan in plans.Skip(1))
                    {
                        foreach (var employee in plan)
                        {
                            var newPeriod = Shift(new Period()
                            {
                                DateFrom = employee.VacationRates!.First().DateFrom,
                                DateTo = employee.VacationRates!.First().DateTo,
                            }, settings.StepA, false);

                            var employeeWithRates = employeeWithRateList.FirstOrDefault(x => x.Id == employee.Id);
                            var rate = employeeWithRateList!.FirstOrDefault(x => x.Id == employee.Id)!
                                .VacationRates!
                                .FirstOrDefault(x => x.DateFrom >= newPeriod.DateFrom && x.DateTo <= newPeriod.DateTo);

                            if (rate != null)
                            {
                                //rounging
                                var roundingPeriod = GetPeriod(rate.DateFrom, rate.DateTo, rate.Days);

                                rate.DateFrom = roundingPeriod.DateFrom;
                                rate.DateTo = roundingPeriod.DateTo;

                                employee.VacationRates = new List<VacationRate>()
                                {
                                    rate
                                };

                            }
                        }
                        var fitFunction = GetFitnessFunction(plan);
                        if(fitFunction > maxFitnessFunction)
                        {
                            maxFitnessFunction = fitFunction;
                            alhpaPlan = plan;
                            j = settings.TMax;
                            break;
                        }
                    }

                }
                
                //refresh
                for(int j =0; j< plans.Count(); j++)
                {
                    var needRefresh = false;
                    foreach(var department in plans[j].GroupBy(x => x.DepartmentId))
                    {
                        if (minEmployeeNeedInDepartnemtList.TryGetValue(department.Key.Value, out var minEmployee) 
                            && minEmployee > 0 && !CheckIfIsMinEmployeeInDepartment(department.Select(x =>x).ToList(), minEmployee, department.Count()))
                        {
                            needRefresh = true;
                            break;
                        }
                    }

                    if(needRefresh)
                    {
                        plans[j] = GetPlan(employeeWithRateList);
                    }
                    else
                    {
                        var fitFunction = GetFitnessFunction(alhpaPlan);

                        if(fitFunction > maxFitnessFunction)
                        {
                            alhpaPlan = plans[j];
                            maxFitnessFunction = fitFunction;
                        }
                    }
                }


                if(maxFitnessFunction == maxRate || i == settings.Max)
                {
                    break;
                }
            }

            var createAt = DateTime.Now;
            foreach (var employee in alhpaPlan)
            {
                await _vacationPlanRepository.AddAsync(new VacationPlan()
                {
                    CrerateAt = createAt,
                    DateFrom = employee.VacationRates.First().DateFrom,
                    DateTo = employee.VacationRates.First().DateTo,
                    UserId = employee.Id
                });
            }

            await _vacationPlanRepository.SaveChangesAsync();
        }

        private int GetFitnessFunction(List<User> plan)
        {
            var sum = 0;

            foreach(var p in plan)
            {
                sum += p.VacationRates!.First().Rate;
            }

            return sum;
        }

        private async Task<Dictionary<int, int>> GetMinEmployeeNeedInDepartnemt()
        {
            return await _departmentRepository.
                Query()
                .Where(x => x.MinEmployeeNeed.HasValue && x.MinEmployeeNeed > 0)
                .ToDictionaryAsync(x => x.Id, y => y.MinEmployeeNeed!.Value);
        }

        private async Task<IEnumerable<User>> GetEmployeeWithVacationRate()
        {
            return await _userRepository
                .Query()
                .Include(x => x.VacationRates)
                .Where(x => x.VacationRates.Any())
                .ToListAsync();
        }

        private List<User> GetPlan(IEnumerable<User> employeeWithVacationRate)
        {
            var currentPlan = new List<User>();
            foreach (var employee in employeeWithVacationRate)
            {

                var rnd = Random.Shared.Next(0, employee.VacationRates.Count());
                var rate = (employee.VacationRates.ToList())[rnd];

                //rounging
                var roundingPeriod = GetPeriod(rate.DateFrom, rate.DateTo, rate.Days);

                rate.DateFrom = roundingPeriod.DateFrom;
                rate.DateTo = roundingPeriod.DateTo;

                currentPlan.Add(new User()
                {
                    Id = employee.Id,
                    DepartmentId = employee.DepartmentId,
                    VacationRates = new List<VacationRate>()
                    {
                        rate
                    }
                });
            }

            return currentPlan;
        }

        private bool CheckIfIsMinEmployeeInDepartment(List<User> plan, int minEmployeeNeed, int maxInDepartment)
        {
            int count = 0;

            foreach(var planI in plan)
            {
                foreach (var planJ in plan)
                {
                    if (planI.Id!= planJ.Id &&
                        CheckDateIntersection(planI.VacationRates.First().DateFrom,
                                                planI.VacationRates.First().DateTo,
                                                planJ.VacationRates.First().DateFrom,
                                                planJ.VacationRates.First().DateTo))
                    {
                        count++;
                    }
                }

                if (count > maxInDepartment - minEmployeeNeed)
                {
                    return false;
                }

                count = 0;
            }

            return true;
        }

        private bool CheckDateIntersection(DateTime dateFrom1, DateTime dateTo1, DateTime dateFrom2, DateTime dateTo2)
        {
            return (dateTo1 >= dateFrom2 && dateTo2 >= dateFrom1);
        }

        private Period GetPeriod(DateTime dateFrom, DateTime dateTo, int days)
        {
            var remainder = (dateTo - dateFrom).Days - days;

            if(remainder <= 0)
            {
                return new Period()
                {
                    DateFrom = dateFrom,
                    DateTo = dateTo
                };
            }

            var move = Random.Shared.Next(0, remainder);

            return new Period()
            {
                DateFrom = dateFrom.AddDays(move),
                DateTo = dateFrom.AddDays(move + days-1)
            };
        }

        private Period Shift(Period current, int step, bool needCkeckBorder, Period all = null)
        {
            var direction = (Random.Shared.Next(2) == 0) ? -1: 1;

            if(needCkeckBorder && current.DateFrom.Date == all.DateFrom.Date)
            {
                direction = 1;
            }
            else if(needCkeckBorder && current.DateTo.Date == all.DateTo.Date)
            {
                direction = -1;
            }

            return new Period()
            {
                DateFrom = current.DateFrom.AddDays(step * direction),
                DateTo = current.DateTo.AddDays(step * direction)
            };
        }
    }
}
