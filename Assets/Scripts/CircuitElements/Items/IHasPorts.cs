public interface IHasPorts
{
    public Port GetPort(ItemPortData itemPortData);

    public ItemPortData GetItemPortData(Port port);
}