using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;

namespace EfTest
{
	internal class TestClass
	{
		public void CreateUsers()
		{
			using (var ctx = new DataContext())
			{
				var u = new User
				{
					Name = "user 1",
					HomeAddress =
					{
						City = "city 1",
						State = "state 1",
						Street = "street 1",
						Phone1 = {Extension = "ext 1", Number = "num 1"},
						Phone2 = {Extension = "ext 2", Number = "num 2"}
					},
					WorkAddress =
					{
						City = "workCity 1",
						State = "workState 1",
						Street = "workStreet 1",
						Phone1 = {Extension = "ext 3", Number = "num 3"},
						Phone2 = {Extension = "ext 4", Number = "num 4"}
					}
				};
				ctx.Users.Add(u);

				var u2 = new User
				{
					Name = "user 2",
					HomeAddress = {City = "city 2", State = "state 2", Street = "street 2"},
					WorkAddress = {City = "workCity 2", State = "workState 2", Street = "workStreet 2"}
				};
				ctx.Users.Add(u2);

				ctx.SaveChanges();
			}
		}

		public void Test()
		{
			using (var ctx = new DataContext())
			{
				ctx.Configuration.ProxyCreationEnabled = false;
				ctx.Configuration.LazyLoadingEnabled = false;

				var oc = ((IObjectContextAdapter) ctx).ObjectContext;
				oc.SavingChanges += OnSavingChanges;

				var u = ctx.Users.ToArray();
				u[0].HomeAddress.Phone2.Extension = "rrr";

				ctx.SaveChanges();
			}
		}

		private void OnSavingChanges(object sender, EventArgs e)
		{
			var oc = (ObjectContext) sender;

			oc.DetectChanges();

			foreach (var entry in oc.ObjectStateManager.GetObjectStateEntries(EntityState.Modified))
			{
				foreach (var prop in entry.GetModifiedProperties())
				{
					var ov = entry.OriginalValues[prop];
					var dr = ov as DbDataRecord;
					if (dr != null)
					{
						var cv = (DbDataRecord) entry.CurrentValues[prop];

						for (var i = 0; i < dr.FieldCount; i++)
						{
							var old = dr.GetValue(i);
							var @new = cv.GetValue(i);

							if (!ReferenceEquals(old, @new))
							{
								Debug.WriteLine("{0} {1} {2} {3}", prop, dr.GetName(i), old, @new);
							}
						}
					}
				}
			}
		}
	}
}