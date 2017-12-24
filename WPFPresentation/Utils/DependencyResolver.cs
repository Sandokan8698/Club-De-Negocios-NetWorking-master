using Data_Layer;
using Data_Layer.Abstract;
using Data_Layer.Implementations;
using WPFPresentation.Models.Provider;

namespace WPFPresentation.Utils
{
    public sealed class DependencyResolver
    {
        #region Data
        static readonly DependencyResolver instance = new DependencyResolver();
        private IUnitOfWork unitOfWork;
              

        #endregion

        #region Ctor

        //CTORs
        static DependencyResolver()
        {

        }

        private DependencyResolver()
        {
            unitOfWork = new UnitOfWork(new ClubNegociosNetworkingContext());
            FacadeProvider = new FacadeProvider();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// La instancia del singleton
        /// </summary>
        public static DependencyResolver Instance
        {
            get { return instance; }
        }

        public FacadeProvider FacadeProvider { get;}

        #endregion
    }
}
