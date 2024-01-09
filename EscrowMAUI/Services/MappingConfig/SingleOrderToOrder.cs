using EscrowMAUI.Models;
using Mapster;

namespace EscrowMAUI.Services.MappingConfig;

class SingleOrderToOrder : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SingleOrderDetail, Order>()
            .Map(dest => dest.Cost, src => src.Cost / 100)
            .Map(dest => dest.BidsCount, src => src.Bids.Count);
    }
}
