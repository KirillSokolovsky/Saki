using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    public interface IData { }
    public interface IItem<out TData> where TData : IData { }

    public abstract class ItemData : IData { }
    public abstract class BaseTreeItem<TData> : IItem<TData> where TData : IData { }
    public sealed class TreeItem : BaseTreeItem<ItemData> { }


    public abstract class WebItemData : ItemData { }
    public abstract class WebElementData : WebItemData { }
    public abstract class WebPageData : WebItemData { }
    public sealed class WebTreeItem<TWebItemData> : BaseTreeItem<TWebItemData>
        where TWebItemData : WebItemData
    { }


    //common defenition for all create requests
    public interface ICreateTreeItemRequest<out TResult> 
        : ISakiRequest<ISakiResult<TResult>>
        where TResult : IItem<IData>
    { }

    [SakiRequestHandlerInfo(HandledCommandName = "CreateTreeItem", ProcessingDataType = typeof(ItemData))]
    //base create tree item handler
    public class CreateTreeItemHandler :
        ISakiRequestHandler<ICreateTreeItemRequest<IItem<IData>>, ISakiResult<IItem<IData>>>
    {
        public Task<ISakiResult<IItem<IData>>> Handle(ICreateTreeItemRequest<IItem<IData>> request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Hadnler: CreateTreeItemHandler");

            return Task.FromResult<ISakiResult<IItem<IData>>>(new SakiResult<IItem<IData>>());
        }
    }

    [SakiRequestInfo(CommandName = "CreateTreeItem", DataType = typeof(ItemData))]
    public class CreateTreeItemRequest : ICreateTreeItemRequest<TreeItem>
    {

    }

    [SakiRequestInfo(CommandName = "CreateTreeItem", DataType = typeof(WebItemData))]
    public class CreateTreeWebItemRequest<TWebItemData> : ICreateTreeItemRequest<WebTreeItem<TWebItemData>>
        where TWebItemData : WebItemData
    {
    }

    [SakiRequestHandlerInfo(HandledCommandName = "CreateTreeItem", ProcessingDataType = typeof(WebItemData))]
    public class CreateWebTreeItemHandler<TWebItemData> :
        ISakiRequestHandler<ICreateTreeItemRequest<WebTreeItem<TWebItemData>>, ISakiResult<WebTreeItem<TWebItemData>>>
        where TWebItemData : WebItemData
    {
        public string HandledCommandName => "CreateTreeItem";
        public string AvailableForItemTypeName => typeof(WebItemData).FullName;

        public Task<ISakiResult<WebTreeItem<TWebItemData>>> Handle(ICreateTreeItemRequest<WebTreeItem<TWebItemData>> request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}