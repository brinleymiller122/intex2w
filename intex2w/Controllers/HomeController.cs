
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using intex2w.Data;
using intex2w.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace intex2w.Controllers
{
    public class HomeController : Controller
    {
        private InferenceSession _session;
        private readonly ILogger<HomeController> _logger;
        private DBContext _context;

        public HomeController(ILogger<HomeController> logger, DBContext context, InferenceSession session)
        {
            _logger = logger;
            _context = context;
            _session = session;
        }

        [HttpGet]
        public IActionResult Index(string city=" ")
        {
            ViewBag.cities = _context.crashes.Select(c => c.CITY).Distinct().OrderBy(c => c);
            List <Crash> TableInfo = _context.crashes.Where(c => c.CITY == city).ToList();
            ViewBag.selectedCity = city;
            ViewBag.TableInfo = TableInfo;

            var wzr = new Dictionary<string, int>();

            foreach (var t in TableInfo)
            {
                if (t.WORK_ZONE_BOOL == true)
                {
                    if (wzr.ContainsKey("Work Zone Related"))
                    {
                        wzr["Work Zone Related"] += 1;
                    }
                    else
                    {
                        wzr.Add("Work Zone Related", 1);
                    }
                }
                if (t.PEDESTRIAN_INVOLVED == true)
                {
                    if (wzr.ContainsKey("Pedestrian Involved"))
                    {
                        wzr["Pedestrian Involved"] += 1;
                    }
                    else
                    {
                        wzr.Add("Pedestrian Involved", 1);
                    }
                }
                if (t.BICYCLIST_INVOLVED == true)
                {
                    if (wzr.ContainsKey("Bicyclist Involved"))
                    {
                        wzr["Bicyclist Involved"] += 1;
                    }
                    else
                    {
                        wzr.Add("Bicyclist Involved", 1);
                    }
                }
                if (t.MOTORCYCLE_INVOLVED == true)
                {
                    if (wzr.ContainsKey("Motorcycle Involved"))
                    {
                        wzr["Motorcycle Involved"] += 1;
                    }
                    else
                    {
                        wzr.Add("Motorcycle Involved", 1);
                    }
                }
                if (t.DUI == true)
                {
                    if (wzr.ContainsKey("DUI"))
                    {
                        wzr["DUI"] += 1;
                    }
                    else
                    {
                        wzr.Add("DUI", 1);
                    }
                }
                if (t.INTERSECTION_RELATED == true)
                {
                    if (wzr.ContainsKey("Intersection Related"))
                    {
                        wzr["Intersection Related"] += 1;
                    }
                    else
                    {
                        wzr.Add("Intersection Related", 1);
                    }
                }
                if (t.WILD_ANIMAL_RELATED == true)
                {
                    if (wzr.ContainsKey("Wild Animal Related"))
                    {
                        wzr["Wild Animal Related"] += 1;
                    }
                    else
                    {
                        wzr.Add("Wild Animal Related", 1);
                    }
                }
                if (t.DOMESTIC_ANIMAL_RELATED == true)
                {
                    if (wzr.ContainsKey("Domestic Animal Related"))
                    {
                        wzr["Domestic Animal Related"] += 1;
                    }
                    else
                    {
                        wzr.Add("Domestic Animal Related", 1);
                    }
                }
                if (t.COMMERCIAL_MOTOR_VEH_INVOLVED == true)
                {
                    if (wzr.ContainsKey("Commercial Motor Vehicle Involved"))
                    {
                        wzr["Commercial Motor Vehicle Involved"] += 1;
                    }
                    else
                    {
                        wzr.Add("Commercial Motor Vehicle Involved", 1);
                    }
                }
                if (t.TEENAGE_DRIVER_INVOLVED == true)
                {
                    if (wzr.ContainsKey("Teenage Driver Involved"))
                    {
                        wzr["Teenage Driver Involved"] += 1;
                    }
                    else
                    {
                        wzr.Add("Teenage Driver Involved", 1);
                    }
                }
                if (t.OLDER_DRIVER_INVOLVED == true)
                {
                    if (wzr.ContainsKey("Older Driver Involved"))
                    {
                        wzr["Older Driver Involved"] += 1;
                    }
                    else
                    {
                        wzr.Add("Older Driver Involved", 1);
                    }
                }
                if (t.NIGHT_DARK_CONDITION == true)
                {
                    if (wzr.ContainsKey("Night/Dark Conditions"))
                    {
                        wzr["Night/Dark Conditions"] += 1;
                    }
                    else
                    {
                        wzr.Add("Night/Dark Conditions", 1);
                    }
                }
                if (t.DISTRACTED_DRIVING == true)
                {
                    if (wzr.ContainsKey("Distracted Driving"))
                    {
                        wzr["Distracted Driving"] += 1;
                    }
                    else
                    {
                        wzr.Add("Distracted Driving", 1);
                    }
                }
                if (t.DROWSY_DRIVING == true)
                {
                    if (wzr.ContainsKey("Drowsy Driving"))
                    {
                        wzr["Drowsy Driving"] += 1;
                    }
                    else
                    {
                        wzr.Add("Drowsy Driving", 1);
                    }
                }
            }

            IEnumerable<KeyValuePair<string, int>> returnable = wzr.OrderBy(key => key.Value).Take(5);
            ViewBag.top5 = returnable;

            return View();
        }
        public IActionResult Prevention()
        {
            return View();
        }
        public IActionResult Regulators()
        {
            return View();
        }

        public IActionResult Crashes(int page = 1, string date = "", string city = " ", string county="", int severity = -1, string timeOfDay = "")
        {
            List<Crash> returnable = new List<Crash>();
            var crashes = _context.crashes.AsQueryable();
            if (date != null && date != "")
            {
                DateTime comparison = DateTime.Parse(date).Date;
                crashes = crashes.ToList().Where(c => c.CRASH_DATE.Date == comparison).AsQueryable();
                ViewBag.selectedDate = comparison;
            }
            if (city != null && city != " " && crashes.Count() > 0)
            {
                crashes = crashes.Where(c => c.CITY == city).AsQueryable();
            }
            if (county != null && county != "" && crashes.Count() > 0)
            {
                crashes = crashes.Where(c => c.COUNTY_NAME == county).AsQueryable();
            }
            if (severity != -1 && crashes.Count() > 0)
            {
                crashes = crashes.Where(c => c.CRASH_SEVERITY_ID == severity).AsQueryable();
            }
            if (timeOfDay != "" && timeOfDay != " " && timeOfDay != null)
            {
                string[] timeArray = timeOfDay.Split("-");
                TimeSpan start = DateTime.Parse(timeArray[0]).TimeOfDay;
                TimeSpan end = DateTime.Parse(timeArray[1]).TimeOfDay;
                
                foreach (Crash crash in crashes)
                {
                    if (crash.CRASH_DATE.TimeOfDay > start && crash.CRASH_DATE.TimeOfDay < end)
                    {
                        returnable.Add(crash);
                    }
                }
            }
            else
            {
                returnable.AddRange(crashes);
            }
            
            if (returnable.ToList().Count() > 0)
            {
                ViewBag.crashes = returnable.Skip((page - 1) * 15).Take(15).ToList().OrderBy(c => c.CRASH_DATE);
            }
            else
            {
                ViewBag.crashes = new List<Crash>();
            }

            List<KeyValuePair<string, string>> times = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Morning Commute (6:00AM - 9:00AM)","06:00-09:00"),
                new KeyValuePair<string, string>("Midday (9:00AM - 4:00PM)","09:00-16:00"),
                new KeyValuePair<string, string>("Evening Commute (4:00PM - 7:00PM)","16:00-19:00"),
                new KeyValuePair<string, string>("Night (7:00PM - 12:00PM)","19:00-24:00"),
                new KeyValuePair<string, string>("Early Morning (12:00PM - 6:00AM)","00:00-6:00"),
            };

            //ViewBag lists for dropdowns
            ViewBag.times = times;
            ViewBag.cities = _context.crashes.Select(c => c.CITY).Distinct().OrderBy(c => c);
            ViewBag.counties = _context.crashes.Select(c => c.COUNTY_NAME).Distinct().OrderBy(c => c);
            ViewBag.severity = _context.crashes.Select(c => c.CRASH_SEVERITY_ID).Distinct().OrderBy(c => c);

            //ViewBag for pagination
            ViewBag.page = page;
            ViewBag.totalPages = (crashes.ToList().Count()) / 10 != 0 ? (crashes.ToList().Count()) / 10 : 1;

            //ViewBag for setting dropdown values
            ViewBag.selectedCity = city ?? " ";
            ViewBag.selectedCounty = county;
            ViewBag.selectedSeverity = severity;
            ViewBag.selectedTimeOfDay = timeOfDay;

            return View();
        }

        [Authorize(Roles="Administrator")]
        [HttpGet]
        public IActionResult Delete(int CRASH_ID)
        {
            Crash crash = _context.crashes.First(c => c.CRASH_ID == CRASH_ID);
            return View(crash);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Delete(int crashID, string y = "")
        {
            Crash c = _context.crashes.First(c => c.CRASH_ID == crashID);
            _context.crashes.Remove(c);
            _context.SaveChanges();
            return RedirectToAction("Crashes");
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Edit(int CRASH_ID = -1)
        {
            if (ModelState.IsValid)
            {
                ViewBag.severity = _context.crashes.Select(c => c.CRASH_SEVERITY_ID).Distinct().OrderBy(c => c);
                ViewBag.cities = _context.crashes.Select(c => c.CITY).Distinct().OrderBy(c => c);
                ViewBag.counties = _context.crashes.Select(c => c.COUNTY_NAME).Distinct().OrderBy(c => c);
                if (CRASH_ID != -1 && CRASH_ID !=0)
                {
                    Crash crash = _context.crashes.First(c => c.CRASH_ID == CRASH_ID);
                    ViewBag.function = "edit";
                    return View(crash);
                }
                else
                {
                    Crash crash = new Crash();
                    crash.CRASH_DATETIME = DateTime.Today.ToShortDateString();
                    ViewBag.function = "add";
                    return View(crash);
                }
            }
            else
            {
                ViewBag.severity = _context.crashes.Select(c => c.CRASH_SEVERITY_ID).Distinct().OrderBy(c => c);
                ViewBag.cities = _context.crashes.Select(c => c.CITY).Distinct().OrderBy(c => c);
                ViewBag.counties = _context.crashes.Select(c => c.COUNTY_NAME).Distinct().OrderBy(c => c);
                return View();
            }

        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Edit(Crash crash, string workZone)
        {
            if (ModelState.IsValid)
            {
                if (workZone == "on")
                {
                    crash.WORK_ZONE_RELATED = "True";
                }
                else
                {
                    crash.WORK_ZONE_RELATED = "False";
                }

                _context.crashes.Update(crash);
                _context.SaveChanges();

                return RedirectToAction("Crashes");
            }
            else
            {
                ViewBag.function = "edit";
                ViewBag.severity = _context.crashes.Select(c => c.CRASH_SEVERITY_ID).Distinct().OrderBy(c => c);
                ViewBag.cities = _context.crashes.Select(c => c.CITY).Distinct().OrderBy(c => c);
                ViewBag.counties = _context.crashes.Select(c => c.COUNTY_NAME).Distinct().OrderBy(c => c);
                
                return View(crash);
            }
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult Add(Crash crash, string workZone)
        {
            if (ModelState.IsValid)
            {
                if (workZone == "on")
                {
                    crash.WORK_ZONE_RELATED = "True";
                }
                else
                {
                    crash.WORK_ZONE_RELATED = "False";
                }

                _context.crashes.Add(crash);
                _context.SaveChanges();
                return RedirectToAction("Crashes");
            }
            else
            {
                ViewBag.function = "add";
                ViewBag.severity = _context.crashes.Select(c => c.CRASH_SEVERITY_ID).Distinct().OrderBy(c => c);
                ViewBag.cities = _context.crashes.Select(c => c.CITY).Distinct().OrderBy(c => c);
                ViewBag.counties = _context.crashes.Select(c => c.COUNTY_NAME).Distinct().OrderBy(c => c);

                return RedirectToAction("Edit",crash);
            }
        }

        [HttpGet]
        public IActionResult Predictor(int CRASH_ID)
        {
            Crash crash = _context.crashes.First(c => c.CRASH_ID == CRASH_ID);
            
            ViewBag.severity = _context.crashes.Select(c => c.CRASH_SEVERITY_ID).Distinct().OrderBy(c => c);
            ViewBag.cities = _context.crashes.Select(c => c.CITY).Distinct().OrderBy(c => c);
            ViewBag.counties = _context.crashes.Select(c => c.COUNTY_NAME).Distinct().OrderBy(c => c);
             
            return View(crash);
        }


        [HttpPost]
        public JsonResult Score(MachineLearning data)
        {
            MachineLearning inputData = new MachineLearning();

            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", inputData.AsTensor())
            });
            string score = result.First().AsTensor<long>().ToArray<long>()[0].ToString();
            
            var prediction = new Prediction { PredictedValue = score };
            result.Dispose();
            return new JsonResult(score);
        }
    }
}
