using System.Web.Mvc;
using MCC.Domain;

namespace MCC.Controllers
{
    public class TranslationController : Controller
    {
        //[Authorize(Roles = "Translatior")]
        public void Save(Translation model)
        {
            TranslationRepository.Save(model);
        }
    }


}