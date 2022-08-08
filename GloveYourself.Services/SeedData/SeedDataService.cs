using GloveYourself.Data.Data;
using GloveYourself.Models.Category;
using GloveYourself.Models.Glove;
using GloveYourself.Models.Task;
using GloveYourself.Services.Category;
using GloveYourself.Services.Glove;
using GloveYourself.Services.Task;

namespace GloveYourself.Services.SeedData
{
    public class SeedDataService : ISeedDataService
    {
        private readonly ApplicationDbContext _context;
        //private readonly ICategoryService _categoryService;
        //private readonly IGloveService _gloveService;
        //private readonly ITaskService _taskService;

        public SeedDataService(ApplicationDbContext context)
        {
            _context = context;
            //_categoryService = categoryService;
            //_gloveService = gloveService;
            //_taskService = taskService;
        }

        public async Task<bool> SeedCategories()
        {
            var _categoryService = new CategoryService(_context);

            int items = _context.Categories.Count();
            if (items == 0)
            {
                var disposable = new CategoryCreate()
                {
                    CategoryName = "Disposable",
                    Description = "Gloves designed for single use only"
                };

                var leather = new CategoryCreate()
                {
                    CategoryName = "Leather",
                    Description = "Gloves made with over 50% leather material"
                };

                var cutResistant = new CategoryCreate()
                {
                    CategoryName = "Cut-Resistant",
                    Description = "ANSI / ISEA Cut Level A1 through A9"
                };

                var nitrileDipped = new CategoryCreate()
                {
                    CategoryName = "Nitrile Dipped",
                    Description = "Gloves dipped in liquid nitrile for better chemical resistance"
                };

                var allPurpose = new CategoryCreate()
                {
                    CategoryName = "All-Purpose",
                    Description = "Reusable gloves designed for general tasks"
                };

                var impactResistant = new CategoryCreate()
                {
                    CategoryName = "Impact-Resistant",
                    Description = "Gloves that incorporate molded rubber components to protect from impact and crushing"
                };

                var latexGrip = new CategoryCreate()
                {
                    CategoryName = "Latex Grip",
                    Description = "A collection of knitted gloves dipped in liquid latex to enhance grip"
                };

                _categoryService.CreateCategory(disposable);
                _categoryService.CreateCategory(leather);
                _categoryService.CreateCategory(cutResistant);
                _categoryService.CreateCategory(nitrileDipped);
                _categoryService.CreateCategory(allPurpose);
                _categoryService.CreateCategory(impactResistant);
                return _categoryService.CreateCategory(latexGrip);
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> SeedTasks()
        {
            var _taskService = new TaskService(_context);

            int items = _context.Tasks.Count();
            if (items == 0)
            {
                var painting = new TaskCreate()
                {
                    TaskName = "Painting",
                };

                var plumbing = new TaskCreate()
                {
                    TaskName = "Plumbing",
                };

                var patientExam = new TaskCreate()
                {
                    TaskName = "Patient Examination",
                };

                var salon = new TaskCreate()
                {
                    TaskName = "Salon",
                };

                var coldStorage = new TaskCreate()
                {
                    TaskName = "Cold Storage",
                };

                var construction = new TaskCreate()
                {
                    TaskName = "Construction",
                };

                var demo = new TaskCreate()
                {
                    TaskName = "Demolition",
                };

                var landscaping = new TaskCreate()
                {
                    TaskName = "Landscaping",
                };

                var firstResponders = new TaskCreate()
                {
                    TaskName = "First Responders",
                };

                var heavyEquipment = new TaskCreate()
                {
                    TaskName = "Heavy Equipment",
                };

                var mechanics = new TaskCreate()
                {
                    TaskName = "Mechanics",
                };

                _taskService.CreateTask(painting);
                _taskService.CreateTask(plumbing);
                _taskService.CreateTask(patientExam);
                _taskService.CreateTask(salon);
                _taskService.CreateTask(coldStorage);
                _taskService.CreateTask(construction);
                _taskService.CreateTask(demo);
                _taskService.CreateTask(landscaping);
                _taskService.CreateTask(firstResponders);
                _taskService.CreateTask(heavyEquipment);
                return _taskService.CreateTask(mechanics);
            }
            else
            {
                return false;
            }
        }
    }
}