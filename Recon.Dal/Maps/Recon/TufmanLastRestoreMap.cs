using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Recon.Domain.Recon;

namespace Recon.Dal.Maps.Recon
{
    class TufmanLastRestoreMap : ClassMap<TufmanLastRestore>
    {
        public TufmanLastRestoreMap()
        {
            ReadOnly();
            Schema("dbo");
            Table("tufman_last_restore");
            Id(x => x.Code).Column("tufman_code");
            Map(x => x.RestoreDate).Column("restore_date");
        }
    }
}
