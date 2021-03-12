﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage_G5.Data;
using Garage_G5.Models;
using Garage_G5.ViewModels;
using Garage_G5.Models.ViewModels;

namespace Garage_G5.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly Garage_G5Context _context;
        public ParkedVehiclesController(Garage_G5Context context)
        {
            _context = context;
        }

        // GET: ParkedVehicles
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.ParkedVehicle.ToListAsync());
        //}
        //public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        //{
        //    ViewData["CurrentSort"] = sortOrder;
        //    ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
        //    if (searchString != null)
        //    {
        //        pageNumber = 1;

        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }
        //    ViewData["CurrentFilter"] = searchString;
        //    var parkedvehicles = from s in _context.ParkedVehicle
        //                   select s;
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        parkedvehicles = parkedvehicles.Where(s => s.RegistrationNum.Contains(searchString)
        //        || s.Color.Contains(searchString)
        //         || s.Model.Contains(searchString)
        //             || s.Brand.Contains(searchString)
        //         || s.Color.Contains(searchString));

               


        //    }
        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            parkedvehicles = parkedvehicles.OrderByDescending(s => s.RegistrationNum);
        //            break;
        //        case "Date":
        //            parkedvehicles = parkedvehicles.OrderBy(s => s.EnteringTime);
        //            break;
        //        case "date_desc":
        //            parkedvehicles = parkedvehicles.OrderByDescending(s => s.EnteringTime);
        //            break;
        //        default:
        //            parkedvehicles = parkedvehicles.OrderBy(s => s.RegistrationNum);
        //            break;
        //    }
        //    int pageSize = 3;
        //    return View(await PaginatedList<ParkedVehicle>.CreateAsync(parkedvehicles.AsNoTracking(), pageNumber ?? 1, pageSize));
        //}
        public async Task<IActionResult> Index(GeneralInfoViewModel viewModel)
        {
            var vehicles = string.IsNullOrWhiteSpace(viewModel.RegistrationNum) ?
               _context.ParkedVehicle :
               _context.ParkedVehicle.Where(m => m.RegistrationNum.StartsWith(viewModel.RegistrationNum));

               vehicles = viewModel.VehicleType == null ?
                vehicles :
                vehicles.Where(m => m.VehicleType == viewModel.VehicleType);

            var model = new GeneralInfoViewModel
            {
                ParkedVehicles = vehicles,
                Types = await GetGenresAsync()
            };

            return View(model);

           // var model = new GeneralInfoModel
           // {
           //     ParkedVehicles = vehicles,
           //     Types = await GetGenresAsync()
           // };
           //// return View(model);

           // return View(await PaginatedList<GeneralInfoModel>.CreateAsync(vehicles.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        private async Task<IEnumerable<SelectListItem>> GetGenresAsync()
        {
            return await _context.ParkedVehicle
                          .Select(m => m.VehicleType)
                          .Distinct()
                          .Select(g => new SelectListItem
                          {
                              Text = g.ToString(),
                              Value = g.ToString()
                          })
                          .ToListAsync();
        }



        public async Task<IActionResult> ReceiptModel(int id)
        {
            

            if (id == 0)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }
            else
            {
                var nRM = new ReceiptModel
                {
                    RegistrationNum = parkedVehicle.RegistrationNum,
                    VehicleType = parkedVehicle.VehicleType,
                    Id = parkedVehicle.Id,
                    EnteringTime = parkedVehicle.EnteringTime,
                    TotalTimeParked = DateTime.Now - parkedVehicle.EnteringTime,
                    Price = (int)(DateTime.Now - parkedVehicle.EnteringTime).TotalMinutes * 10 / 60,
                    //Price = (int)
                    
                };
                return View(nRM);
            }
        

    
      }

   


        // GET: ParkedVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleType,RegistrationNum,Color,Brand,Model,WheelsNum,EnteringTime")] ParkedVehicle parkedVehicle)
        {

            if (ModelState.IsValid)
            {
                _context.Add(parkedVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleType,RegistrationNum,Color,Brand,Model,WheelsNum,EnteringTime")] ParkedVehicle parkedVehicle)
        {
            if (id != parkedVehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkedVehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkedVehicleExists(parkedVehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            _context.ParkedVehicle.Remove(parkedVehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkedVehicleExists(int id)
        {
            return _context.ParkedVehicle.Any(e => e.Id == id);
        }
  
        public bool IsRegisterNumberExists(string RegistrationNum)
        {
            return !_context.ParkedVehicle.Any(x => x.RegistrationNum == RegistrationNum);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult IsRegExists(string RegistrationNum, int Id)
        {
            return Json(IsUnique(RegistrationNum, Id));
        }

        private bool IsUnique(string RegistrationNum, int Id)
        {
            if (Id == 0) // its a new object
            {
                return !_context.ParkedVehicle.Any(x => x.RegistrationNum == RegistrationNum);
            }
            else // its an existing object so exclude existing objects with the id
            {
                return !_context.ParkedVehicle.Any(x => x.RegistrationNum == RegistrationNum && x.Id != Id);
            }
        }
    }

}
