namespace Saki.Framework.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISakiCommand
    {
        string CommandName { get; }
        ISakiTreeState TreeState { get; }
    }
}
