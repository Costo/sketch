using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using SketchCore.Web.Services;

namespace SketchCore.Web.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly IImportService _importService;

        public IndexModel(IImportService importService)
        {
            _importService = importService;
        }

        [BindProperty]
        public InputModel Model { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await _importService.DeepImport(Model.Url);
            }
            catch(Exception e)
            {
                ModelState.AddModelError("Model.Url", e.Message);
            }
            return Page();
        }

        public class InputModel
        {
            [Required]
            public string Url { get; set; }
        }
    }
}