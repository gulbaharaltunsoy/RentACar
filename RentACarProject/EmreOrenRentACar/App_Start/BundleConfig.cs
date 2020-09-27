using System.Web;
using System.Web.Optimization;

namespace EmreOrenRentACar
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Style").Include(
                       "~/Assets/Css/bootstrap.min.css",
                       "~/Assets/Css/custom.css"));

        }
    }
}
