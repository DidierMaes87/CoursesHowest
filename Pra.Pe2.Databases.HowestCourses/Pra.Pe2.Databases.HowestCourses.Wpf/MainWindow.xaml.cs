using Pra.Pe2.Databases.HowestCourses.Core.Entities;
using Pra.Pe2.Databases.HowestCourses.Core.Interfaces;
using Pra.Pe2.Databases.HowestCourses.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pra.Pe2.Databases.HowestCourses.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IHowestService howestService = new DapperSyncService();

        private List<Lecturer> lecturers;
       

        public MainWindow()
        {
            InitializeComponent();
        }

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateLecturers();
            PopulateProgram();
            DisableBoxAndButtons();

            lstLecturers.SelectedItem = txtFirstname.Text;
            
        }

        // XAML methods

        private void DisableBoxAndButtons()
        {
            cmbCurriculum.IsEnabled = false;
            cmbLecturers.IsEnabled = false;
            btnSaveCourse.IsEnabled = false;
        }

        private void RefreshTxtBoxes()
        {
            txtFirstname.Text = string.Empty;
            txtLastName.Text = string.Empty;
        }

        // Populate methods

        private void PopulateLecturers()
        {

            lecturers = howestService.GetLecturers();

            lstLecturers.ItemsSource = lecturers;
            lstLecturers.Items.Refresh();
            cmbLecturers.ItemsSource = lecturers;
        }


        private void PopulateSemesters()
        {
            string fieldOfStudy = ((FieldOfStudy)cmbPrograms.SelectedItem)?.Program;
            List<Curriculum> semesters = howestService.GetSemesters(fieldOfStudy);

            cmbCurriculum.ItemsSource = semesters;
            cmbCurriculum.Items.Refresh();
        }

        private void PopulateCourses()
        {
            string fieldOfStudy = ((FieldOfStudy)cmbPrograms.SelectedItem).Program;
            int semester = cmbCurriculum.SelectedIndex + 1;


            lstCourses.ItemsSource = howestService.GetCourses(fieldOfStudy, semester);

        }

        private void PopulateProgram()
        {
            List<FieldOfStudy> programs = howestService.GetPrograms();
            cmbPrograms.ItemsSource = programs;
            cmbPrograms.Items.Refresh();
        }



        // CRUD LECTURES

        private void BtnAddLecturer_Click(object sender, RoutedEventArgs e)
        {
            string firstName = txtFirstname.Text.Trim();
            string lastName = txtLastName.Text.Trim();



            if (firstName.Length == 0 || lastName.Length == 0)

            {
                MessageBox.Show("Je dient een naam op te geven indien je iemand wil toevoegen");
                txtFirstname.Focus();
                return;
            }

            Lecturer lecturer = new Lecturer(firstName, lastName);

            if (!howestService.AddLecturer(lecturer))
            {
                MessageBox.Show("Er kan geen nieuwe docent toegevoegd worden");
                txtFirstname.Focus();
                return;
            }
            lstLecturers.Items.Refresh();
            PopulateLecturers();
            RefreshTxtBoxes();
        }

        private void BtnEditLecturer_Click(object sender, RoutedEventArgs e)
        {
            if (lstLecturers.SelectedItem == null)
            {
                MessageBox.Show("Gelieve een docent te selecteren");
                return;
            }

            string firstName = txtFirstname.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            if (firstName.Length == 0 || lastName.Length == 0)
            {
                MessageBox.Show("Gelieve een naam in te geven aub");
                txtFirstname.Focus();
                return;
            }
            Lecturer lecturer = (Lecturer)lstLecturers.SelectedItem;
            lecturer.FirstName = firstName;
            lecturer.LastName = lastName;

            if (!howestService.UpdateLecturer(lecturer))
            {
                MessageBox.Show("Lector niet gewijzigd");
                txtFirstname.Focus();
                return;
            }
            PopulateLecturers();
            lstLecturers.SelectedItem = lecturer;
            RefreshTxtBoxes();

        }

        private void BtnDeleteLecturer_Click(object sender, RoutedEventArgs e)
        {


            if (lstLecturers.SelectedItem == null)
            {
                MessageBox.Show("Gelieve een docent te selecteren");
            }
            Lecturer lecturer = (Lecturer)lstLecturers.SelectedItem;

            if (!howestService.DeleteLecturer(lecturer))
            {
                MessageBox.Show("Docent niet verwijderd, want reeds gekoppeld aan een vak");

            }

            PopulateLecturers();
            RefreshTxtBoxes();
        }

        // COMBOBOXEN PROGRAMS - CURRICULUM

        private void CmbPrograms_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            
            PopulateSemesters();
            cmbCurriculum.IsEnabled = true;
        }

        private void CmbCurriculum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateCourses();
            cmbLecturers.SelectedIndex = -1;
            
        }


        // SELECTION CHANGED FROM LISTS

        private void LstLecturers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstLecturers.SelectedItem != null)
            {
                Lecturer lecturer = (Lecturer)lstLecturers.SelectedItem;
                txtFirstname.Text = lecturer.FirstName;
                txtLastName.Text = lecturer.LastName;
            }

        }

        private void LstCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbLecturers.IsEnabled = true;
            btnSaveCourse.IsEnabled = true;

            if (lstCourses.SelectedItem != null)
            {
                Courses courses = (Courses)lstCourses.SelectedItem;


                cmbLecturers.SelectedIndex = lecturers.IndexOf(lecturers.FirstOrDefault(x => x.ID == courses.CoordinatorID));
            }
        }


        // SAVE BUTTON

        private void BtnSaveCourse_Click(object sender, RoutedEventArgs e)
        {
            Lecturer lecturer = (Lecturer)cmbLecturers.SelectedItem;

            if (cmbLecturers.SelectedItem == null)
            {
                MessageBox.Show("Gelieve een docent te kiezen");
                return;
            }

            if (!howestService.UpdateLecturer(lecturer))
            {
                MessageBox.Show("Lector niet toegewezen");
                return;
            }
        }

    }
    
}
