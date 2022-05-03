using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Test
{
    public partial class Form1 : Form
    {
        public SqlConnection myConnection; //setup sql objects
        public SqlCommand myCommand;
        public SqlDataReader myReader;
        //String sql, Output = "";
        public Form1()
        {
            InitializeComponent();
           
            String connectionString = "Data Source=DT-ALEX;Initial Catalog=Pokemon Project;Trusted_Connection = yes;";
            SqlConnection myConnection = new SqlConnection(connectionString);

            try  //fills datagrid for list pokemon page
            {
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from[Pokemon Project].[dbo].[Pokedex]", myConnection);
                DataTable dt = new DataTable();
                sqlDa.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString(), "Error");
                MessageBox.Show("Error: Could not connect to Database", "Error");

                this.Close();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try //Search pokemon by ID
            {
                String connectionString = "Data Source=DT-ALEX;Initial Catalog=Pokemon Project;Trusted_Connection = yes;";
                SqlConnection myConnection = new SqlConnection(connectionString);

                string Substring = textBox1.Text;
                SqlDataAdapter sqlDa = new SqlDataAdapter($"Select * from[Pokemon Project].[dbo].[Pokedex] where id={Substring}", myConnection);

                DataTable dt = new DataTable();
                sqlDa.Fill(dt);
                dataGridView2.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Invalid Input", "Warning");

            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            try //Search pokemon by name
            {
                String connectionString = "Data Source=DT-ALEX;Initial Catalog=Pokemon Project;Trusted_Connection = yes;";
                SqlConnection myConnection = new SqlConnection(connectionString);

                string Substring2 = "%" + textBox2.Text + "%";

                SqlDataAdapter sqlDa = new SqlDataAdapter($"Select * from[Pokemon Project].[dbo].[Pokedex] where name like '{Substring2}'", myConnection);

                DataTable dt = new DataTable();
                sqlDa.Fill(dt);
                dataGridView2.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Invalid Input", "Warning");
                //MessageBox.Show($"{ex}", "Error");

            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                String connectionString = "Data Source=DT-ALEX;Initial Catalog=Pokemon Project;Trusted_Connection = yes;";
                SqlConnection myConnection = new SqlConnection(connectionString);

                string Substring = textBox3.Text;


                SqlDataAdapter sqlDa = new SqlDataAdapter($"Select * from[Pokemon Project].[dbo].[moves] where id={Substring}", myConnection);

                DataTable dt = new DataTable();
                sqlDa.Fill(dt);
                dataGridView3.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Invalid Input", "Warning");

            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            String connectionString = "Data Source=DT-ALEX;Initial Catalog=Pokemon Project;Trusted_Connection = yes;";
            SqlConnection myConnection = new SqlConnection(connectionString);

            string Substring2 = "%" + textBox4.Text + "%";

            try
            {
                SqlDataAdapter sqlDa = new SqlDataAdapter($"Select * from[Pokemon Project].[dbo].[moves] where identifier like '{Substring2}' ", myConnection);

                DataTable dt = new DataTable();
                sqlDa.Fill(dt);
                dataGridView3.DataSource = dt;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Invalid Input", "Warning");

            }
        }
        private void button5_Click_1(object sender, EventArgs e)
        {

            label6.Text = "Type 1: "; //clear info labels
            label7.Text = "Type 2: ";
            label8.Text = "Name: ";

            string[] types = new string[] {"normal","fighting","flying","poison","ground","rock","bug",
                "ghost","metal","fire","water","plant","lightning","psychic","ice","dragon","dark","fairy"};

            string type_mark1, type_mark2, poke_name, move_name; //temp vars for db values

            string index1 = " "; string index2 = " ";


            String connectionString = "Data Source=DT-ALEX;Initial Catalog=Pokemon Project;Trusted_Connection = yes;";
            SqlConnection myConnection = new SqlConnection(connectionString);
            try
            {
                string Substring = textBox5.Text;
                if (Substring == "") //if move entry is empty
                {
                    MessageBox.Show("Invalid Input", "Warning");
                }
                else
                {     //type 1
                    SqlDataAdapter sqlDa1 = new SqlDataAdapter($"Select type_id from[Pokemon Project].[dbo].[pokemon_types] " +
                        $"where pokemon_id={Substring} and slot=1", myConnection);


                    DataTable dt1 = new DataTable();
                    sqlDa1.Fill(dt1);
                    //dataGridView4.DataSource = dt;
                    foreach (DataRow dtRow in dt1.Rows)
                    {
                        type_mark1 = dtRow[0].ToString();
                        int int_type_mark = Int32.Parse(type_mark1);

                        label6.Text = "Type 1: " + types[int_type_mark - 1];
                        index1 = int_type_mark.ToString();
                    }

                    //type 2
                    SqlDataAdapter sqlDa2 = new SqlDataAdapter($"Select type_id from[Pokemon Project].[dbo].[pokemon_types] " +
                                $"where pokemon_id={Substring} and slot=2", myConnection);


                    DataTable dt2 = new DataTable();
                    sqlDa2.Fill(dt2);
                    //dataGridView4.DataSource = dt;
                    foreach (DataRow dtRow in dt2.Rows)
                    {
                        type_mark2 = dtRow[0].ToString();
                        int int_type_mark = Int32.Parse(type_mark2);

                        label7.Text = "Type 2: " + types[int_type_mark - 1];
                        index2 = int_type_mark.ToString();

                    }
                    //get name for pokemon
                    SqlDataAdapter sqlDa3 = new SqlDataAdapter($"Select name from [Pokemon Project].[dbo].[Pokedex] " +
                            $"where id={Substring}", myConnection);


                    DataTable dt3 = new DataTable();
                    sqlDa3.Fill(dt3);
                    //dataGridView4.DataSource = dt;
                    foreach (DataRow dtRow in dt3.Rows)
                    {
                        poke_name = dtRow[0].ToString();

                        label8.Text = "Name: " + poke_name;
                    }

                    //get moves
                    SqlDataAdapter sqlDa5 = new SqlDataAdapter($"Select * from [Pokemon Project].[dbo].[moves] " +
                    $"where move_type={index1} or move_type={index2}", myConnection);
                       


                    if (index2 == " ")
                    {  //if type 2 is blank search only type 1 else search both types
                       //MessageBox.Show("Type 2 is blank", "Test");
                        SqlDataAdapter sqlDa4 = new SqlDataAdapter($"Select * from [Pokemon Project].[dbo].[moves] " +
                          $"where move_type={index1}", myConnection);
                        DataTable dt4 = new DataTable();
                        sqlDa4.Fill(dt4);
                        dataGridView4.DataSource = dt4;
                        foreach (DataRow dtRow in dt4.Rows)
                        {
                            move_name = dtRow[0].ToString();

                        }

                    }
                    else if (index1 == " " & index2 == " ")
                    {
                        MessageBox.Show("Invalid Input", "Warning");

                    }
                    else
                    {

                        SqlDataAdapter sqlDa4 = new SqlDataAdapter($"Select * from [Pokemon Project].[dbo].[moves] " +
                        $"where move_type={index1} or move_type={index2}", myConnection);
                        DataTable dt4 = new DataTable();
                        sqlDa4.Fill(dt4);
                        dataGridView4.DataSource = dt4;
                        foreach (DataRow dtRow in dt4.Rows)
                        {
                            move_name = dtRow[0].ToString();

                        }
                    }

                }
            }

            catch (SqlException ex) //This catches incorrect input
            {
                MessageBox.Show("Invalid Input", "Warning");

            }
        }
     
        private void button6_Click(object sender, EventArgs e)
        {   //add button
            try
            {
                String connectionString = "Data Source=DT-ALEX;Initial Catalog=Pokemon Project;Trusted_Connection = yes;";
                SqlConnection myConnection = new SqlConnection(connectionString);

                //Convert Strings to ints
                int id = Convert.ToInt32(textBox6.Text);
                string name = textBox7.Text;
                int species_id = Convert.ToInt32(textBox8.Text);
                int height = Convert.ToInt32(textBox9.Text);
                int weight = Convert.ToInt32(textBox10.Text);
                int base_experience = Convert.ToInt32(textBox11.Text);



                //Button for Creating Pokemon
                SqlDataAdapter sqlDa1 = new SqlDataAdapter($"Insert into [Pokemon Project].[dbo].[Pokedex]  (id,name,species_id,height,weight,base_experience)" +
                    $"values({id}, '{name}',{species_id},{height},{weight},{base_experience})", myConnection);


                DataTable dt1 = new DataTable();
                sqlDa1.Fill(dt1);

                MessageBox.Show($"Successfully added {name} into Database.", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to create new Entry. Check inputs", "Error");
                //MessageBox.Show($"{ex}", "Error");

            }

        }
        private void button7_Click(object sender, EventArgs e)
        {   //Delete Button
            String connectionString = "Data Source=DT-ALEX;Initial Catalog=Pokemon Project;Trusted_Connection = yes;";
            SqlConnection myConnection = new SqlConnection(connectionString);
            string Substring = textBox12.Text;
            try
            {
                SqlDataAdapter sqlDa = new SqlDataAdapter($"Delete [Pokemon Project].[dbo].[Pokedex] where id={Substring}", myConnection);

                DataTable dt1 = new DataTable();
                sqlDa.Fill(dt1);
                MessageBox.Show($"Successfully removed entry from database.", "Success");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not delete entry", "Error");
            }


        }

        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Search for a Pokemon by their ID number or name.", "Help");

        }

        private void button10_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Search for a Pokemon move by their ID number or name.", "Help");

        }

        private void button11_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"List all Pokemon in database.", "Help");

        }

        private void button12_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Shows all moves that a Pokemon can learn. Enter the pokemon by their ID number.", "Help");

        }

        private void button13_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Add a new pokemon entry to the database.", "Help");

        }

        private void button14_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Remove a pokemon entry from the database. Enter their ID number.", "Help");

        }

        private void label17_Click_1(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Update an pokemon entry from the database. Enter the ID of the" +
                $" field to change, select the field you want to update" +
                $" and enter the value.", "Help");

        }

        private void button16_Click(object sender, EventArgs e)
        {   //Update Button
            string id_of_entry = textBox13.Text;
            string new_entry = textBox14.Text;
            string selected_item = listBox1.SelectedItem.ToString();

            //MessageBox.Show(listBox1.SelectedItem.ToString(), "Test");

            String connectionString = "Data Source=DT-ALEX;Initial Catalog=Pokemon Project;Trusted_Connection = yes;";
            SqlConnection myConnection = new SqlConnection(connectionString);

            try            
            {
                SqlDataAdapter sqlDa = new SqlDataAdapter($"Update [Pokemon Project].[dbo].[Pokedex] set {selected_item} = '{new_entry}' where " +
                    $"id={id_of_entry}", myConnection);
                DataTable dt1 = new DataTable();
                sqlDa.Fill(dt1);
                MessageBox.Show($"Successfully updated entry from database.", "Success");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not update entry.", "Error");
                //MessageBox.Show($"{ex}", "Error");

            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Type Legend\n" +
                $"------------------\n" +
                $"1 normal \n2 fighting \n3 flying \n4 poison" +
                $"\n5 ground \n6 rock \n7 bug \n8 ghost \n9 metal " +
                $"\n10 fire \n11 water \n12 plant \n13 lightning \n14 psychic " +
                $"\n15 ice \n16 dragon \n17 dark \n18 fairy", "Help");

        }

        private void button8_Click(object sender, EventArgs e)
        {
            String connectionString = "Data Source=DT-ALEX;Initial Catalog=Pokemon Project;Trusted_Connection = yes;";

            SqlConnection myConnection = new SqlConnection(connectionString);

            try
            {
                //myConnection.Open();
                //myCommand = new SqlCommand();
                //myCommand.Connection = myConnection;

                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * from[Pokemon Project].[dbo].[Pokedex]", myConnection);
                DataTable dt = new DataTable();
                sqlDa.Fill(dt);
                dataGridView1.DataSource = dt;
                MessageBox.Show("Refreshed List to Current List.", "Success");

            }
            catch
            {
                //MessageBox.Show(e.ToString(), "Error");
                MessageBox.Show("Error: Could not connect to Database", "Error");

                this.Close();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {

        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void label8_Click(object sender, EventArgs e)
        {

        }
        private void label9_Click(object sender, EventArgs e)
        {

        }
        private void label7_Click_1(object sender, EventArgs e)
        {

        }
        private void label6_Click_1(object sender, EventArgs e)
        {

        }
        private void label8_Click_1(object sender, EventArgs e)
        {

        }
        private void label9_Click_1(object sender, EventArgs e)
        {

        }
        private void label11_Click(object sender, EventArgs e)
        {

        }
        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }
        private void tabPage5_Click(object sender, EventArgs e)
        {

        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        private void label16_Click(object sender, EventArgs e)
        {

        }
        private void label17_Click(object sender, EventArgs e)
        {

        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void tabPage7_Click(object sender, EventArgs e)
        {

        }
        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }
        private void button18_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Type Legend\n" +
               $"------------------\n" +
               $"1 normal \n2 fighting \n3 flying \n4 poison" +
               $"\n5 ground \n6 rock \n7 bug \n8 ghost \n9 metal " +
               $"\n10 fire \n11 water \n12 plant \n13 lightning \n14 psychic " +
               $"\n15 ice \n16 dragon \n17 dark \n18 fairy", "Help");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Type Legend\n" +
               $"------------------\n" +
               $"1 normal \n2 fighting \n3 flying \n4 poison" +
               $"\n5 ground \n6 rock \n7 bug \n8 ghost \n9 metal " +
               $"\n10 fire \n11 water \n12 plant \n13 lightning \n14 psychic " +
               $"\n15 ice \n16 dragon \n17 dark \n18 fairy", "Help");
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

