using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary1.Tools;
using BabyCarrot_1;
using System.Collections;

namespace WindowsFormsApp1
{
    

    public partial class Form2 : Form
    {
        System.Windows.Forms.Label label;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int result_count = 0;

            string sql = "";
            Hashtable hashtable = new Hashtable();
            
            String text_id = textBox1.Text;

            if (!text_id.Equals(null) && !text_id.Equals(""))
            {
                hashtable.Add("name", text_id);
            }

            create_sql("SELECT name, ID, password FROM Members;", hashtable);

                if (text_id.Equals(null) || text_id.Equals(""))
            {
                result_count = DBManager.Select_Count();
            }
            else
            {
                result_count = DBManager.Select_Count(text_id);
            }

            if(result_count != 0)
            {
                
                sql = create_sql("SELECT ID, name, nickname, password password FROM Members ", hashtable);

            
                List<Hashtable> hs = DBManager.Select(sql);

                
            }

            for(int i = 0; i < result_count; i++)
            {
                label = new System.Windows.Forms.Label();
                label.Location = new System.Drawing.Point(30, 150 + i * 30);
                label.Name = "label" + i.ToString();
                label.Size = new System.Drawing.Size(100, 23);

            }


        }

        private string create_sql(string sql, Hashtable table)
        {
            string sql_para = "";
            foreach(DictionaryEntry entry in table)
            {
                if(!"".Equals(entry.Value)&&null!=entry.Value)
                    sql_para = " and " + entry.Key + " = @" + entry.Value;
            }

            return sql + " where " + sql_para + " ;";
            
        }
    }
}
