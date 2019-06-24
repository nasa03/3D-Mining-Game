public interface IBlockItem
{
    int blockId { get; set; }
    BlockInfo blockinfo { get; set; }

    BlockInfo getBlockInfo();
}