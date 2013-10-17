using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SyncingShip2_0.Models
{
    public class Template1Repository : ITemplate1Repository
    {
        private List<Template1> templates = new List<Template1>();
        private int _nextId = 1;

        public Template1Repository()
        {
            Add(new Template1 { FirstName = "TestFirst1", LastName = "LastFirst1", BirthDate = new DateTime(01,01,01), Item1 = "Test Item 1" });
            Add(new Template1 { FirstName = "TestFirst2", LastName = "LastFirst2", BirthDate = new DateTime(02, 02, 02), Item1 = "Test Item 2" });
            Add(new Template1 { FirstName = "TestFirst3", LastName = "LastFirst3", BirthDate = new DateTime(03, 03, 03), Item1 = "Test Item 3" });
        }


        public IEnumerable<Template1> GetAll()
        {
            return templates;
        }

        public Template1 Get(int id)
        {
            return templates.Find(p => p.Id == id);
        }

        public Template1 Add(Template1 item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            item.Id = _nextId++;
            templates.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            templates.RemoveAll(p => p.Id == id);
        }

        public bool Update(Template1 item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            
            int index = templates.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            templates.RemoveAt(index);
            templates.Add(item);

            return true;
        }
    }
}