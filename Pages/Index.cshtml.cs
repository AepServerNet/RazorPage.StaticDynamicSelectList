using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelectListMvc_Load_Static_Dynamic.Models.Enums;
using SelectListMvc_Load_Static_Dynamic.Models.Services.Application;
using SelectListMvc_Load_Static_Dynamic.Models.Services.Infrastructure;
using SelectListMvc_Load_Static_Dynamic.Models.ViewModels;

namespace SelectListMvc_Load_Static_Dynamic.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DemoDbContext dbContext;
        public IndexModel(DemoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [BindProperty]
        public int manager { get; set; }

        public Utenti Utenti { get; set; }

        public SelectList UsersList { get; set; }
        public SelectList PeopleList { get; set; }

        public ListViewModel<UserViewModel> Users { get; set; }

        public async Task<IActionResult> OnGetAsync([FromServices] IUserService userService)
        {
            await LoadDataAsync(userService);

            Users = await userService.GetListaUsers(0);
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int manager, [FromServices] IUserService userService)
        {
            await LoadDataAsync(userService);

            Users = await userService.GetListaUsers(manager);
            
            return Page();
        }

        public async Task LoadDataAsync(IUserService userService)
        {
            List<UserViewModel> listUsers = await userService.GetUsersFromDatabase();
            List<UserViewModel> listPeople = await userService.GetPeopleFromDatabase();

            UsersList = new SelectList(listUsers, "Id", "Nominativo");

            PeopleList = new SelectList(listPeople, "Id", "Nominativo");
        }
    }
}