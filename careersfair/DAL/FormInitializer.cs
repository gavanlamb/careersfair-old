using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using careersfair.Models;

namespace careersfair.DAL{
    public class FormInitializer : System.Data.Entity. DropCreateDatabaseIfModelChanges<FormContext>{
        protected override void Seed(FormContext context){
            var forms = new List<Form>{
                new Form{Name="AUT 2011",TableName="aut2011",Structure="structure"},
                new Form{Name="AUT 2012",TableName="aut2012",Structure="structure"},
                new Form{Name="AUT 2013",TableName="aut2013",Structure="structure"},
                new Form{Name="AUT 2014",TableName="aut2014",Structure="structure"},
                new Form{Name="AUT 2015",TableName="aut2015",Structure="structure"},
                new Form{Name="AUT 2016",TableName="aut2016",Structure="structure"},
                new Form{Name="AUT 2017",TableName="aut2017",Structure="structure"},
                new Form{Name="AUT 2018",TableName="aut2018",Structure="structure"},
                new Form{Name="AUT 2019",TableName="aut2019",Structure="structure"},
                new Form{Name="AUT 2020",TableName="aut2020",Structure="structure"}
            };
            forms.ForEach(f => context.Form.Add(f));
            context.SaveChanges();
        }
    }
}
