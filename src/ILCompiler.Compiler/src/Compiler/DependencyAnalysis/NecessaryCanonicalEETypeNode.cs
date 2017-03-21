﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;

using Internal.TypeSystem;

namespace ILCompiler.DependencyAnalysis
{
  /// <summary>
    /// The node is used in ProjectX to represent a canonical type that does not have a vtable.
    /// </summary>
    internal sealed class NecessaryCanonicalEETypeNode : EETypeNode
    {
        public NecessaryCanonicalEETypeNode(NodeFactory factory, TypeDesc type) : base(factory, type)
        {
            //TODO: This is used temporarily until we switch from STS dependency analysis.
            Debug.Assert(factory.Target.Abi == TargetAbi.ProjectN);
            Debug.Assert(!type.IsCanonicalDefinitionType(CanonicalFormKind.Any));
            Debug.Assert(type.IsCanonicalSubtype(CanonicalFormKind.Any));
            Debug.Assert(type == type.ConvertToCanonForm(CanonicalFormKind.Specific));
            Debug.Assert(!type.IsCanonicalSubtype(CanonicalFormKind.Universal));
        }

        protected override ISymbolNode GetBaseTypeNode(NodeFactory factory)
        {
            return _type.BaseType != null ? factory.NecessaryTypeSymbol(GetFullCanonicalTypeForCanonicalType(_type.BaseType)) : null;
        }
    }
}