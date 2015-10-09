using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using careersfair.Models;


///TO BE DELETED WHEN WE PUBLISH, THIS IS FOR TESTING ONLY.
namespace careersfair.DAL
{
    public class FormInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<FormContext>
    {
        protected override void Seed(FormContext context)
        {
            var forms = new List<Form>{
                new Form{Name="AUT 2011",HTML="structure",Enabled=true},
                new Form{Name="AUT 2012",HTML="structure",Enabled=true},
                new Form{Name="AUT 2013",HTML="structure",Enabled=true},
                new Form{Name="AUT 2014",HTML="structure",Enabled=true},
                new Form{Name="AUT 2015",HTML="structure",Enabled=true},
                new Form{Name="AUT 2016",HTML="structure",Enabled=true},
                new Form{Name="AUT 2017",HTML="structure",Enabled=true},
                new Form{Name="AUT 2018",HTML="structure",Enabled=true},
                new Form{Name="AUT 2019",HTML="structure",Enabled=true},
                new Form{Name="AUT 2020",HTML="structure",Enabled=true}
            };
            forms.ForEach(f => context.Form.Add(f));
            context.SaveChanges();
        }
    }
}