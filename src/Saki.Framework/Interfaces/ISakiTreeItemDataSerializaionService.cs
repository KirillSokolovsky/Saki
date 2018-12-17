namespace Saki.Framework.Interfaces
{
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISakiTreeItemDataSerializaionService
    {
        SakiResult<TItemData> DeserializeTreeItemData<TItemData>(string itemDataType, byte[] data)
            where TItemData : ISakiTreeItemData;

        SakiResult<byte[]> SerializeTreeItemData(ISakiTreeItemData itemData);
    }
}
