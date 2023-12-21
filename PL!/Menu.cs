using Bussiness_layer;
using CourseWork_.Entities;
using DLL;
using System.Collections;

namespace PL
{
    enum Entity
    {
        patient, doctor
    };
    public class Menu
    {
        string FilePath = EntityContext.CreateFile(File());
        string SchedulePath = EntityContext.CreateFile("Schedule.json");
        ArrayList list=new ArrayList();
        ArrayList schedule = new ArrayList();
        bool finished = false;
        string date = "";
        string time = "";
        public Menu() { }
        public void ShowMenu()
        {
            int input=0;
            bool err = false;
            do
            {
                do
                {
                    try
                    {
                        err = false;
                        bool innerErr = false;
                        Console.WriteLine("Enter:\n\r" +
                            "1. to manage doctor\n\r" +
                            "2. to manage patient\n\r" +
                            "3. to manage schedule\n\r" +
                            "4. to quit");
                        input = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                        switch (input)
                        {
                            case 1://to manage doctor
                                do
                                {
                                    innerErr = false;
                                    Console.Clear();
                                    try
                                    {
                                        ManageDoctor( ref list);
                                    }
                                    catch (Exception e)
                                    {
                                        innerErr = true;
                                        Console.WriteLine(e.Message);
                                    }
                                } while (innerErr);
                                break;
                            case 2://to manage patient
                                bool innerErr2 = false;
                                do
                                {
                                    innerErr2 = false;
                                    Console.WriteLine("Enter:\n\r" +
                                   "1. to register a patient\n\r" +
                                   "2. to delete a patient\n\r" +
                                   "3. to change data about the patient\n\r"+
                                   "3. to view an electronic card of the patient");
                                    try
                                    {
                                        input = Convert.ToInt32(Console.ReadLine());
                                        ManagePatient(input, ref list);
                                    }
                                    catch (Exception e)
                                    {
                                        innerErr2 = true;
                                        Console.WriteLine(e.Message);
                                    }
                                } while (innerErr2);
                                break;
                            case 3:// to manage schedule
                                Console.WriteLine("Enter:\n\r" +
                                   "1. to make an appointment \n\r" +
                                   "2. to change the schedule\n\r" +
                                   "3. to search for patient\n\r"+
                                   "4. to search for doctor's schedule\r\n"+
                                   "5. to search for doctor");
                                input = Convert.ToInt32(Console.ReadLine());
                                ManageSchedule(input, ref list);
                                break;
                            case 4:
                                Console.WriteLine("Program finished successfully!");
                                finished = true;
                                EntityContext.DeleteFile(FilePath);
                                EntityContext.DeleteFile(SchedulePath);
                                break;
                            default:
                                throw new IndexOutOfRangeException();
                            } 
                    }
                    catch(Exception e)
                    {
                        err = true;
                        Console.WriteLine(e.Message);
                    }
                } while (err);
            } while (!finished);

        }
        static string File()
        {
            string file = "";
            string extension = "";
            bool err = false;
            do
            {
                err = false;
                try
                {
                    Console.WriteLine("Enter name of the file:");
                     file = Console.ReadLine();
                    if (file == "")
                    {
                        throw new InvalidDataException();
                    }
                }
                catch(Exception e)
                {
                    err = true;
                    Console.WriteLine(e.Message);
                }
            } while (err);
            do
            {
                err = false;
                try
                {
                    Console.WriteLine("Enter extension:\n"+
                        "1.Binary\n\r"+
                        "2.Json\n\r"+
                        "3.Xml\n\r"+
                        "4.Custom");
                    int input = Convert.ToInt32(Console.ReadLine());
                    switch (input)
                    {
                        case 1:
                           extension= ".bin";
                            break;
                        case 2:
                            extension =".json";
                            break;
                        case 3:
                            extension = ".xml";
                            break;
                        case 4:
                            extension = ".dat";
                            break;
                        default:
                            throw new IndexOutOfRangeException();

                    }

                }
                catch(Exception e)
                {
                    err = true;
                    Console.WriteLine(e.Message);
                }
            } while (err);
            return file + extension;
        }
        void ManageDoctor(ref ArrayList list)
        {
            int input = 0;
            bool err = false;
            Console.WriteLine("Enter:\n\r" +
                        "1. to add a doctor\n\r" +
                        "2. to delete a doctor\n\r" +
                        "3. to change data about the doctor\n\r" +
                        "4. to view all doctors and their specialties");
            do
            {
                input = Convert.ToInt32(Console.ReadLine());
                err = false;
                try
                {
                    switch (input)
                    {
                        case 1://to add a doctor
                            Doctor added = Doctor.AddedDoctor(CreateField("name"), CreateField("surname"), CreateField("specialization"), ref  list,  FilePath);
                            Console.WriteLine($"Doctor {added.Name} {added.Surname} added to the list");
                            break;
                        case 2://to delete a doctor
                            Console.WriteLine("Enter to delete doctor:");
                            ArrayList indexes = new ArrayList();
                            for (int i = 0; i < list.Count; i++)
                            {
                                if (list[i] is Doctor)
                                {
                                    Console.WriteLine($"{i}.{list[i]}");
                                    indexes.Add(i);
                                }
                                do
                                {
                                    err = false;
                                    try
                                    {
                                        int num = Convert.ToInt32(Console.ReadLine());
                                        if (!indexes.Contains(num)) { throw new IndexOutOfRangeException(); }
                                        Doctor deleted=Doctor.DeleteDoctor(num, ref list, FilePath);
                                        Console.WriteLine($"Doctor {deleted.Name} {deleted.Surname} deleted from the list");
                                    }
                                    catch (Exception e)
                                    {
                                        err = true;
                                        Console.WriteLine(e.Message);
                                        Console.WriteLine("Enter correct index:");
                                    }
                                } while (err);
                            }
                            break;
                        case 3://to change data about the doctor
                            ChangeData(Entity.doctor, "specialization");
                            break;
                        case 4:// to view all doctors and their specializations
                            Console.WriteLine("Doctors and their specializations:");
                            if (list != null)
                            {
                                foreach (Human doc in list)
                                {
                                    if (doc is Doctor)
                                    {
                                        Console.WriteLine(doc);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("List is empty");
                            }

                            break;
                        default:
                            throw new IndexOutOfRangeException();
                    }
                }
                catch(Exception e)
                {
                    err = true;
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Enter correct index:");
                }
            } while (err);
        }
        void ManagePatient(int input, ref ArrayList list)
        {
            bool err = false;
            Console.Clear();
            do
            {
                err = false;
                switch (input)
                {
                    case 1:// to register a patient
                        string name = CreateField("name");
                        string surname = CreateField("surname");
                        string diagnosis = CreateField("diagnosis");
                        Patient added = Patient.AddPatient(name, surname, diagnosis, ref list, FilePath);
                        Console.WriteLine($"Patient {added.Name} {added.Surname} added to list");
                        break;
                    case 2://to delete a patient
                        Console.WriteLine("Enter to delete a patient:");
                        ArrayList indexes = new ArrayList();
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (list[i] is Patient)
                            {
                                Console.WriteLine($"{i}.{list[i]}");
                                indexes.Add(i);
                            }
                            do
                            {
                                err = false;
                                try
                                {
                                    int num = Convert.ToInt32(Console.ReadLine());
                                    if (!indexes.Contains(num)) { throw new IndexOutOfRangeException(); }
                                    Patient deleted = Patient.DeletePatient(num, ref list, FilePath);
                                    Console.Write($"Patient {deleted.Name} {deleted.Surname} deleted from the list");
                                }
                                catch (Exception e)
                                {
                                    err = true;
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine("Enter correct index:");
                                }
                            } while (err);
                        }
                        break;
                    case 3://to change data about the patient
                        ChangeData(Entity.patient, "diagnosis");
                        break;
                    case 4://  to view an electronic card of the patient
                        Console.WriteLine("Enter to view an electronic card of the patient:");
                        ArrayList iOfPatients = new ArrayList();
                        if (list != null)
                        {
                            Console.WriteLine(Patient.ViewElectronicCard(ref iOfPatients, ref list, ref  schedule));
                            do
                            {
                                try
                                {
                                    err = false;
                                    input = Convert.ToInt32(Console.ReadLine());
                                    if (!iOfPatients.Contains(input))
                                    {
                                        throw new IndexOutOfRangeException();
                                    }
                                    Patient patient = (Patient)list[input];
                                    Console.WriteLine(patient);
                                }
                                catch(Exception e)
                                {
                                    err = true;
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine("Enter correct index:");
                                }
                            } while (err);
                        }
                        else
                        {
                            Console.WriteLine("List is empty");
                        }
                        break;
                    default:
                        err = true;
                        Console.WriteLine("Enter correct index:");
                        break;
                }
            } while (err);
        }
        void ManageSchedule(int input, ref ArrayList list)
        {
            Console.Clear();
            bool err = false;
            do
            {
                switch (input)
                {
                    case 1:// to add a schedule
                        
                        if (list.Count == null)
                        {
                            Console.WriteLine("Appointment was not scheduled because of the absence of patients and doctors");
                            break;
                        }
                        CreateDate(ref date);
                        Console.WriteLine("Choose time:");
                        CreateTime(ref time);
                        Doctor doctor = (Doctor)ChooseEntity(Entity.doctor);//choose a doctor
                        if (doctor == null)
                        {
                            Console.WriteLine("Appointment was not scheduled because of the absence of doctors");
                            break;
                        }
                        Patient patient = (Patient)ChooseEntity(Entity.patient);//choose a patient
                        if (doctor != null && patient == null)
                        {
                            Console.WriteLine("Appointment was not scheduled because of the absence of patients");
                            break;
                        }
                        Schedule.MakeAppointment( time, date, doctor, patient, ref schedule, SchedulePath);
                        break;
                    case 2:// to change the schedule
                        do
                        {
                            err = false;
                            try
                            {
                                Console.WriteLine("Enter:\r\n" +
                                "1. to delete an appointment\r\n" +
                                "2. to change time of the appointment\r\n"+
                                "3. to change date of the appointment");
                                input = Convert.ToInt32(Console.ReadLine());
                                switch (input)
                                {
                                    case 1://delete an appointment

                                        for (int i = 0; i < schedule.Count; i++)
                                        {
                                            Console.WriteLine($"{i}.{schedule[i]}");
                                        }
                                        do
                                        {
                                            err = false;
                                            try
                                            {
                                                Console.WriteLine("Enter index:");
                                                input = Convert.ToInt32(Console.ReadLine());
                                                if (input >= schedule.Count || input < 0)
                                                {
                                                    throw new IndexOutOfRangeException();
                                                }
                                                Schedule.DeleteSchedule(ref schedule,  input,  SchedulePath);
                                            }
                                            catch (Exception e)
                                            {
                                                err = true;
                                                Console.WriteLine(e.Message);
                                            }
                                        } while (err);

                                        break;
                                    case 2://to change time of the appointment
                                        err = false;
                                        Console.WriteLine("Enter to change time of the appointment: ");
                                        Schedule currentSchedule;
                                        do
                                        {
                                            try
                                            {
                                                currentSchedule = ChooseSchedule();
                                                Console.WriteLine("Enter new time:");
                                                CreateTime(ref time);
                                                Schedule.ChangeTime(time, currentSchedule, schedule, SchedulePath);
                                            }
                                            catch(Exception e)
                                            {
                                                err = true;
                                                Console.WriteLine(e.Message);
                                            }
                                        } while (err);

                                        break;
                                    case 3://to change date of the appointment
                                        err = false;
                                        Console.WriteLine("Enter to change date of the appointment: ");
                                        do
                                        {
                                            try
                                            {
                                                currentSchedule = ChooseSchedule();
                                                CreateDate(ref date);
                                                Schedule.ChangeDate(date, currentSchedule, schedule, SchedulePath);
                                            }
                                            catch (Exception e)
                                            {
                                                err = true;
                                                Console.WriteLine(e.Message);
                                            }
                                        } while (err);

                                        break;
                                    default:
                                        throw new IndexOutOfRangeException();
                                }
                            }
                            catch (Exception e)
                            {
                                err = true;
                                Console.WriteLine(e.Message);
                            }
                        } while (err);
                        break;
                    case 3:// find patient by name and surname
                        string name = CreateField("name");
                        string surname = CreateField("surname");
                        bool isPatient = false;
                        foreach (Human human in list)
                        {
                            if (human.Name == name && human.Surname == surname)
                            {
                                isPatient = true;
                            }
                        }
                        Console.Clear();
                        string foundPatient = Schedule.FindPatient(name, surname, schedule);
                        if (foundPatient == null&& isPatient)
                        {
                            Console.WriteLine($"{name} {surname} does not have an appointment");
                            break;
                        }
                        if (foundPatient == null && !isPatient)
                        {
                            Console.WriteLine($"{name} {surname} is not a patient");
                            break;
                        }
                        Console.WriteLine(foundPatient);
                        break;
                    case 4: //to search for doctor's schedule
                        if (schedule.Count ==0)
                        {
                            Console.WriteLine("List is empty");
                            break;
                        }
                        Console.WriteLine("Enter time of  doctor's schedule");
                        time = "";
                        do
                        {
                            err = false;
                            try
                            {
                                time = Console.ReadLine();
                                if (!Exceptions.validTime.IsMatch(time))
                                {
                                    throw new Exception("Incorrect time format");
                                }
                            }
                            catch (Exception e)
                            {
                                err = true;
                                Console.WriteLine(e.Message);
                                Console.WriteLine("Choose correct time:");
                            }
                        } while (err);
                        string doctorsSchedule=Schedule.DoctorsSchedule(CreateField("name"), CreateField("surname"),time, schedule);
                        if (doctorsSchedule == null)
                        {
                            Console.WriteLine("Doctor not found");
                            break;
                        }
                        Console.WriteLine(doctorsSchedule);
                        break;
                    case 5:// searching for doctor
                         name = CreateField("name");
                         surname = CreateField("surname");
                        bool isDoctor=false;
                        string foundDoctor = Schedule.FindDoctor(name, surname, schedule);
                        foreach(Human human in list)
                        {
                            if(human.Name==name && human.Surname == surname)
                            {
                                isDoctor = true;
                            }
                        }
                        if (foundDoctor == null && isDoctor)
                        {
                            Console.WriteLine($"{name} {surname} does not have appointments");
                            break;
                        }
                        if(foundDoctor==null && !isDoctor)
                        {
                            Console.WriteLine($"{name} {surname} is not a doctor");
                            break;
                        }
                        Console.WriteLine(foundDoctor);
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            } while (err);
        }
        string CreateField(string typeOfField)
        {
            bool err = false;
            string field = "";
            do
            {
                err = false;
                try
                {
                    Console.WriteLine($"Enter {typeOfField}:");
                     field = Console.ReadLine();
                    if (!Exceptions.validInput.IsMatch(field) || field == "")
                    {
                        throw new InvalidDataException();
                    }
                }
                catch (Exception e)
                {
                    err = true;
                    Console.WriteLine(e.Message);
                }
            } while (err);
            return field;
        }
        Human ChooseEntity(Entity entity)
        {
            bool err = false;
            int num = 0;
            if (list.Count == 0)
            {
                Console.WriteLine($"List of {entity}s empty");
                return null;
            }
            Console.WriteLine($"Choose {entity}:");
            ArrayList index = new ArrayList();
            for (int i = 0; i < list.Count; i++)
            {
                if (Convert.ToString(entity) == "doctor")
                {
                    if (list[i] is Doctor)
                    {
                        Doctor p = (Doctor)list[i];
                        Console.WriteLine($"{i}.{p.Name} {p.Surname}");
                        index.Add(i);
                    }
                }
                if (Convert.ToString(entity) == "patient")
                {
                    if (list[i] is Patient)
                    {
                        Patient p = (Patient)list[i];
                        Console.WriteLine($"{i}.{p.Name} {p.Surname}");
                        index.Add(i);
                    }
                }
            }
            if (index.Count == 0)
            {
                Console.WriteLine($"List of {entity}s is empty");
                return null;
            }
            do
            {
                err = false;
                try
                {
                    num = Convert.ToInt32(Console.ReadLine());
                    if (!index.Contains(num)) { throw new IndexOutOfRangeException(); }
                }
                catch (Exception e)
                {
                    err = true;
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Enter correct index:");
                }
            } while (err);
            return (Human)list[num];
        }
        void CreateTime(ref string time)
        {
            bool err = false;
            do
            {
                try
                {
                    err = false;
                    time = Console.ReadLine();
                    if (!Exceptions.validTime.IsMatch(time))
                    {
                        throw new Exception("Incorrect time format");
                    }
                    foreach (Schedule s in schedule)
                    {
                        if (s.Time == time)
                        {
                            throw new Exception("Current time is not available");
                        }
                    }
                }
                catch (Exception e)
                {
                    err = true;
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Choose correct time:");
                }
            } while (err);
        }
        void CreateDate(ref string date)
        {
            bool err = false;
            do// create date of an appointment
            {
                err = false;
                try
                {
                    Console.WriteLine("Enter date of an appointment:");
                    date = Console.ReadLine();
                    if (!Exceptions.vaidDate.IsMatch(date))
                    {
                        throw new Exception("Invalid data format!");
                    }
                }
                catch (Exception e)
                {
                    err = true;
                    Console.WriteLine(e.Message);
                }
            } while (err);
        }
        void ChangeData(Entity entity, string specialField)
        {
            bool err;
            ArrayList index = new ArrayList();
            int ind = -1;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] is Doctor && Convert.ToString(entity) == "doctor")
                {
                    Console.WriteLine($"{i}.{list[i]}");
                    index.Add(i);
                }
                if (list[i] is Patient && Convert.ToString(entity) == "patient")
                {
                    Console.WriteLine($"{i}.{list[i]}");
                    index.Add(i);
                }
            }
                do
                {
                    err = false;
                    try
                    {
                        Console.WriteLine($"Enter to change data about the {entity}:");
                        ind = Convert.ToInt32(Console.ReadLine());
                        if (!index.Contains(ind)) { throw new IndexOutOfRangeException(); }
                    }
                    catch (Exception e)
                    {
                        err = true;
                        Console.WriteLine(e.Message);
                    }
                } while (err);

                Console.Clear();
                Console.WriteLine("Enter to change:\n\r" +
                    "1. name\n\r" +
                    "2. surname\r\n"+ 
                    $"3. {specialField}");
            do
            {
                err = false;
                Human localHuman = (Human)list[ind];
                try
                {
                    int num = Convert.ToInt32(Console.ReadLine());
                    switch (num)
                    {
                        case 1:
                            localHuman.Name = CreateField("new name");
                            break;
                        case 2:
                            localHuman.Surname = CreateField("new surname");
                            break;
                        case 3:
                            localHuman.SpecialField = CreateField($"new {specialField}");
                            break;
                        default:
                            throw new IndexOutOfRangeException();
                    }
                    list[ind] = (Convert.ToString(entity) == "doctor") ? new Doctor(localHuman.Name, localHuman.Surname, localHuman.SpecialField) : new Patient(localHuman.Name, localHuman.Surname, localHuman.SpecialField);
                    EntityContext.WriteFile(list, FilePath);
                }
                catch (Exception e)
                {
                    err = true;
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Enter correct index:");
                }
            } while (err);
            
        }
        Schedule ChooseSchedule()
        {
            bool err = false;
            int input=-1;
            do
            {
                err = false;
                try
                {
                    if (schedule == null)
                    {
                        Console.WriteLine("No appointments were made");
                        break;
                    }
                    Console.WriteLine("Enter to choose schedule:");
                    for (int i = 0; i < schedule.Count; i++)
                    {
                        Console.WriteLine($"{i}.{schedule[i]}");
                    }
                    input = Convert.ToInt32(Console.ReadLine());
                    if(input>=schedule.Count || input < 0)
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
                catch(Exception e)
                {
                    err = true;
                    Console.WriteLine(e.Message);
                }
            } while (err);
            return (Schedule)schedule[input];
        }
    }
}
