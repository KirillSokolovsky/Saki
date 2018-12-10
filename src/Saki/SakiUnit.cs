namespace Saki
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public struct SakiUnit : IEquatable<SakiUnit>, IComparable<SakiUnit>, IComparable
    {
        public static readonly SakiUnit Value = new SakiUnit();
        public static readonly Task<SakiUnit> Task = System.Threading.Tasks.Task.FromResult(Value);
        public int CompareTo(SakiUnit other)
        {
            return 0;
        }
        int IComparable.CompareTo(object obj)
        {
            return 0;
        }
        public override int GetHashCode()
        {
            return 0;
        }
        public bool Equals(SakiUnit other)
        {
            return true;
        }
        public override bool Equals(object obj)
        {
            return obj is SakiUnit;
        }
        public static bool operator ==(SakiUnit what, SakiUnit with)
        {
            return true;
        }
        public static bool operator !=(SakiUnit what, SakiUnit with)
        {
            return false;
        }
        public override string ToString()
        {
            return "()";
        }
    }
}
