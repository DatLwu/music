using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using demo.Data;
using demo.Models;

namespace demo.Controllers
{
    public class songsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public songsController(ApplicationDbContext context)
        {
            _context = context;
        }
        

        // GET: songs
        public async Task<IActionResult> Index(string searchString)
        {
            // Số lượng bài hát muốn hiển thị trong top songs
            int topSongsCount = 10;

            var songs = from s in _context.songs.Include(s => s.Category)
                        select s;

            if (!String.IsNullOrEmpty(searchString))
        {
            string searchLower = searchString.ToLower();
            songs = songs.Where(s => s.Song_Name.ToLower().Contains(searchString) ||
                                    s.Artist.ToLower().Contains(searchString) ||
                                    s.Category.CategoryName.ToLower().Contains(searchString));
        }

            var topSongs = await songs
                .OrderByDescending(s => s.ListenCount)
                .Take(topSongsCount)
                .ToListAsync();

            // Truyền danh sách top songs vào ViewBag
            ViewBag.TopSongs = topSongs;
            ViewBag.SearchString = searchString;
            return View(await songs.ToListAsync());
        }


        // GET: songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songs = await _context.songs
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.SongID == id);
            if (songs == null)
            {
                return NotFound();
            }
            await _context.SaveChangesAsync();

            return View(songs);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateListenCount(int id)
        {
            var song = await _context.songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            song.ListenCount++;
            await _context.SaveChangesAsync();

            return Ok();
        }


        // GET: songs/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryID");
            return View();
        }

        // POST: songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SongID,Song_Name,Artist,CategoryID,ListenCount")] songs songs, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "music");
                if (!Directory.Exists(uploadsDirectory))
                {
                    Directory.CreateDirectory(uploadsDirectory);
                }
                var fileName = $"{NormalizeFileName(songs.Song_Name)}.mp3";
                var filePath = Path.Combine(uploadsDirectory, fileName);

                // Lưu file vào thư mục "Uploads/Music"
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                // Lưu đường dẫn tệp vào model
                songs.File_path = fileName;
                songs.ListenCount = 0; // Khởi tạo số lượt nghe

                _context.Add(songs);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Trường hợp không có file được tải lên, hiển thị form với thông báo lỗi
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryID", songs.CategoryID);
            ModelState.AddModelError("File", "Please select a file to upload.");
            return View(songs);
        }
        private string NormalizeFileName(string fileName)
        {
            // Replace spaces with hyphens
            return fileName.Replace(" ", "-");
        }

        // GET: songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songs = await _context.songs.FindAsync(id);
            if (songs == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryID", songs.CategoryID);
            return View(songs);
        }

        // POST: songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SongID,Song_Name,Artist,File_path,CategoryID,ListenCount")] songs songs)
        {
            if (id != songs.SongID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(songs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!songsExists(songs.SongID))
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
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryID", songs.CategoryID);
            return View(songs);
        }

        // GET: songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songs = await _context.songs
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.SongID == id);
            if (songs == null)
            {
                return NotFound();
            }

            return View(songs);
        }

        // POST: songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var songs = await _context.songs.FindAsync(id);
            if (songs != null)
            {
                _context.songs.Remove(songs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool songsExists(int id)
        {
            return _context.songs.Any(e => e.SongID == id);
        }
        
    }
}
