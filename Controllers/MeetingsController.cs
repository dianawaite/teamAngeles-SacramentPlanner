﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SacramentPlanner.Data;
using SacramentPlanner.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace SacramentPlanner.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly SacramentPlannerContext _context;
        public List<string> Bishopric = new List<string>();
        public List<string> Members = new List<string>();
        public List<string> Hymns = new List<string>();
        public List<string> SacramentHymns = new List<string>();
        public MeetingsController(SacramentPlannerContext context)
        {
            _context = context;
        }

        // GET: Meetings
        public async Task<IActionResult> Index()
        {
            return _context.Meeting != null ? 
                          View(await _context.Meeting.ToListAsync()) :
                          Problem("Entity set 'SacramentPlannerContext.Meeting'  is null.");
        }

        // GET: Meetings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Meeting == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meeting
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meeting == null)
            {
                return NotFound();
            }

            // get meeting speakers
            var speakers = _context.Speaker
                                       .Where(s => s.Meeting == id)
                                       .ToList();

            // https://www.tutorialsteacher.com/mvc/viewbag-in-asp.net-mvc
            ViewData["Speakers"] = speakers;

            return View(meeting);
        }

        // GET: Meetings/Create
        public IActionResult Create()
        {
            // get members
            var members = _context.Member
                                       .Where(q => q.Bishopric != true)
                                       .ToList();
            ViewData["Members"] = members;

            // get members of the bishopric
            var bishopric = _context.Member
                                       .Where(q => q.Bishopric == true)
                                       .ToList();
            ViewData["Bishopric"] = bishopric;

            // get hymns
            var hymns = _context.Hymn
                                       .Where(q => q.Sacrament != true)
                                       .ToList();
            ViewData["Hymns"] = hymns;

            // get sacrament hymns
            var sacramentHymns = _context.Hymn
                           .Where(q => q.Sacrament == true)
                           .ToList();
            ViewData["SacramentHymns"] = sacramentHymns;



            var model = new Meeting();

            return View(model);
        }

        // POST: Meetings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection, [Bind("Id,Congregation,MeetingDate,Conducting,OpeningPrayer,ClosingPrayer,OpeningHymn,SacramentHymn,IntermediateHymn,ClosingHymn")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meeting);
                await _context.SaveChangesAsync();

                // get new meeting id
                int id = meeting.Id;
                //System.Diagnostics.Debug.WriteLine($"Id = {id}");
                addSpeakers(id, collection);

                return RedirectToAction(nameof(Index));
            }
            return View(meeting);
        }

        // GET: Meetings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Meeting == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meeting.FindAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }

            // get meeting speakers
            var speakers = _context.Speaker
                                       .Where(s => s.Meeting == id)
                                       .ToList();

            // https://www.tutorialsteacher.com/mvc/viewbag-in-asp.net-mvc
            ViewData["Speakers"] = speakers;
            ViewData["Speaker_Count"] = speakers.Count + 1; // increase count to get correct index when adding new speakers

            return View(meeting);
        }

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection, [Bind("Id,Congregation,MeetingDate,Conducting,OpeningPrayer,ClosingPrayer,OpeningHymn,SacramentHymn,IntermediateHymn,ClosingHymn")] Meeting meeting)
        {
            if (id != meeting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // delete any speakers associated with this meeting
                    deleteSpeakers(id);

                    // add new speakers to meeting
                    addSpeakers(id, collection);

                    _context.Update(meeting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingExists(meeting.Id))
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
            return View(meeting);
        }

        // GET: Meetings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Meeting == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meeting
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meeting == null)
            {
                return NotFound();
            }

            // get meeting speakers
            var speakers = _context.Speaker
                                       .Where(s => s.Meeting == id)
                                       .ToList();

            // https://www.tutorialsteacher.com/mvc/viewbag-in-asp.net-mvc
            ViewData["Speakers"] = speakers;

            return View(meeting);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Meeting == null)
            {
                return Problem("Entity set 'SacramentPlannerContext.Meeting'  is null.");
            }
            var meeting = await _context.Meeting.FindAsync(id);
            if (meeting != null)
            {
                // delete any speakers associated with this meeting
                deleteSpeakers(id);

                _context.Meeting.Remove(meeting);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingExists(int id)
        {
          return (_context.Meeting?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        /**********************************************
         * DELETE SPEAKERS
         * *******************************************/
        private void deleteSpeakers(int id)
        {
            // use where clause to select speakers to delete
            var speakersToDelete = _context.Speaker.Where(r => r.Meeting == id);

            // execute query and retrieve speakers as list
            var speakersList = speakersToDelete.ToList();

            // loop through speakers and delete each one
            foreach (var speaker in speakersList)
            {
                _context.Speaker.Remove(speaker);
            }

            // commit changes to data source
            _context.SaveChanges();
        }

        /**********************************************
         * ADD SPEAKERS
         * *******************************************/
        private void addSpeakers(int id, IFormCollection collection)
        {
            //Speaker newSpeaker = new Speaker();
            string newName = "";
            string newSubject = "";

            foreach (string key in collection.Keys)
            {
                if (key.Contains("Speaker"))
                {
                    System.Diagnostics.Debug.WriteLine($"Speaker = {collection[key]}");
                    newName = collection[key];
                }
                if (key.Contains("Subject"))
                {
                    System.Diagnostics.Debug.WriteLine($"Subject = {collection[key]}");
                    newSubject = collection[key];
                }

                if (newName != "" && newSubject != "")
                {
                    // https://stackoverflow.com/questions/37816067/c-sharp-mvc-entity-framework-only-saving-last-row
                    // need to create a new Speaker here otherwise only the last Speaker is added
                    Speaker newSpeaker = new Speaker();
                    newSpeaker.Meeting = id;
                    newSpeaker.Name = newName;
                    newSpeaker.Subject = newSubject;

                    // TODO: this should only run if current name and subject are set
                    System.Diagnostics.Debug.WriteLine($"Meeting = {id}, Speaker = {newName}, Subject={newSubject}");
                    _context.Speaker.AddRange(newSpeaker);
                    newName = "";
                    newSubject = "";
                }
            }
            _context.SaveChanges();
        }
    }
}
