﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace Loja.Inspiracao.Util.Contracts
{
    public class ExplicitNameContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var list = base.CreateProperties(type, memberSerialization);

            foreach (var prop in list)
            {
                prop.PropertyName = prop.UnderlyingName;
            }

            return list;
        }
    }
}
