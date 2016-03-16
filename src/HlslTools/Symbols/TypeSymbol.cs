﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace HlslTools.Symbols
{
    public abstract class TypeSymbol : Symbol
    {
        private readonly List<Symbol> _members;
        private ImmutableArray<Symbol> _membersArray;
        private bool _lazyMembersComputed;

        public ImmutableArray<Symbol> Members
        {
            get
            {
                if (_membersArray == ImmutableArray<Symbol>.Empty)
                {
                    EnsureLazyMembers();
                    _membersArray = _members.ToImmutableArray();
                }
                return _membersArray;
            }
        }

        private void EnsureLazyMembers()
        {
            if (!_lazyMembersComputed)
            {
                ComputeLazyMembers();
                _lazyMembersComputed = true;
            }
        }

        protected virtual void ComputeLazyMembers()
        {
        }

        protected TypeSymbol(SymbolKind kind, string name, string documentation, Symbol parent)
            : base(kind, name, documentation, parent)
        {
            _members = new List<Symbol>();
        }

        public string FullName
        {
            get
            {
                var result = string.Empty;
                if (Parent != null)
                    result += Parent.Name + "``";
                result += Name;
                return result;
            }
        }

        public virtual TypeSymbol GetBaseType()
        {
            return null;
        }

        public IEnumerable<T> LookupMembers<T>(string name)
            where T : Symbol
        {
            EnsureLazyMembers();
            return _members.Where(x => x is T && x.Name == name).OfType<T>();
        }

        internal void AddMember(Symbol member)
        {
            _members.Add(member);
            // TODO: Add diagnostic if duplicate name.
            _membersArray = ImmutableArray<Symbol>.Empty;
        }
    }
}