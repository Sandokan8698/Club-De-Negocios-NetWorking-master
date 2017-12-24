namespace Data_Layer.Utils.ServiceFilter
{
    /// <summary>
    /// Mensages disponible para eviar y recivir
    /// </summary>
    public enum FilterBuilderTypes
    {
        VentasFilterCreator,
        SubPedidoFilterCreator,
        SubPedidoEntryFilterCreator       
    };

    public sealed class FilterCreatorFactory
    {

        #region Data
        static readonly FilterCreatorFactory instance = new FilterCreatorFactory();
        #endregion

        #region Ctor

        //CTORs
        static FilterCreatorFactory()
        {

        }

        private FilterCreatorFactory()
        {
           
        }
        #endregion


        #region Public Properties
        /// <summary>
        /// La instancia del singleton
        /// </summary>
        public static FilterCreatorFactory Instance
        {
            get { return instance; }
        }

        #endregion

        #region Methods

        public FilterCreator Create(FilterBuilderTypes tipo, FilterEntitie filterEntitie)
        {
            switch (tipo)
            {
                case FilterBuilderTypes.VentasFilterCreator:
                    return new VentasFilterCreator(filterEntitie);

                case FilterBuilderTypes.SubPedidoFilterCreator:
                    return new SubPedidoFilterCreator(filterEntitie);

                case FilterBuilderTypes.SubPedidoEntryFilterCreator:
                    return  new SubPedidoEntryFilterCreator(filterEntitie);

            }

            return null;
        }

        #endregion
    }
}
