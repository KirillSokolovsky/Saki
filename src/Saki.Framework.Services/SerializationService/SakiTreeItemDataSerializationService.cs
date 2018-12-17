namespace Saki.Framework.Services.SerializationService
{
    using Newtonsoft.Json;
    using Saki.Framework.Interfaces;
    using Saki.Framework.Result;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class SakiTreeItemDataSerializationService : ISakiTreeItemDataSerializaionService
    {
        private static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            TypeNameHandling = TypeNameHandling.Arrays | TypeNameHandling.Objects,
            PreserveReferencesHandling = PreserveReferencesHandling.All,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
        };

        public SakiResult<TItemData> DeserializeTreeItemData<TItemData>(string itemDataType, byte[] data) 
            where TItemData : ISakiTreeItemData
        {
            var dataType = Type.GetType(itemDataType);
            var requestedType = typeof(TItemData);
            if (dataType == null)
            {
                var ex = new SakiTreeItemDataSerializationException(nameof(DeserializeTreeItemData),
                    $"Couldn't find item data type: {itemDataType}" +
                    $"{Environment.NewLine}Requested type: {requestedType.FullName}");
                return SakiResult<TItemData>.FromEx(ex);
            }

            if (!requestedType.IsAssignableFrom(dataType))
            {
                var ex = new SakiTreeItemDataSerializationException(nameof(DeserializeTreeItemData),
                    $"Item data type: {itemDataType}" +
                    $"{Environment.NewLine}couldn't be assigned to requested type." +
                    $"{Environment.NewLine}Requested type: {requestedType.FullName}");
                return SakiResult<TItemData>.FromEx(ex);
            }


            try
            {
                var json = Encoding.UTF8.GetString(data);
                var itemData = (TItemData)JsonConvert.DeserializeObject(json, dataType, JsonSerializerSettings);

                return SakiResult<TItemData>.Ok(itemData);
            }
            catch (Exception ex)
            {
                var sakiException = new SakiTreeItemDataSerializationException(nameof(DeserializeTreeItemData),
                    $"Error occurred during deserelization of item data with type: {itemDataType}",
                    ex);

                return SakiResult<TItemData>.FromEx(sakiException);
            }
        }

        public SakiResult<byte[]> SerializeTreeItemData(ISakiTreeItemData itemData)
        {
            try
            {
                var dataJson = JsonConvert.SerializeObject(itemData, JsonSerializerSettings);

                var bytes = Encoding.UTF8.GetBytes(dataJson);

                return SakiResult<byte[]>.Ok(bytes);
            }
            catch (Exception ex)
            {
                var sakiEx = new SakiTreeItemDataSerializationException(nameof(SerializeTreeItemData),
                    $"Error occurred during serelization of item data",
                    ex);

                return SakiResult<byte[]>.FromEx(sakiEx);
            }
        }
    }
}
