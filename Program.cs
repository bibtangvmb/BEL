using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using Component;
using Database;
using System.Threading;

namespace Project_1
{
    class Program
    {
       public static AccessDatabaseHelper DB = new AccessDatabaseHelper("./Jadwal Bintang.accdb");

        static void Main(string[] args)
        {
            new Clear(33,6, 86, 17).SetBackColor(ConsoleColor.DarkCyan).Tampil();
            Tulisan Judul = new Tulisan();
            Judul.Text = "APLIKASI BEL SEKOLAH";
            Judul.X = 50;
            Judul.Y = 1;
            Judul.Tampil();

            Tulisan NamaKampus = new Tulisan();
            NamaKampus.Text = "WEARNES EDUCATION CENTER";
            NamaKampus.X = 48;
            NamaKampus.Y= 2;
            NamaKampus.SetLength(110);
            NamaKampus.SetForeColor(ConsoleColor.Cyan);
            NamaKampus.Tampil();

            Tulisan AlamatCampus = new Tulisan();
            AlamatCampus.Text = "JL.THAMRIN NO.38 MADIUN";
            AlamatCampus.X = 48;
            AlamatCampus.Y = 3;
            AlamatCampus.SetLength(110);
            AlamatCampus.Tampil();

            Tulisan Nama = new Tulisan();
            Nama.Text = "VALENTINO MAULANA BINTANG";
            Nama.X = 49;
            Nama.Y = 26;
            Nama.SetLength(110);
            Nama.Tampil();
            


            Kotak Kepala = new Kotak();
            Kepala.X = 0;
            Kepala.Y = 0;
            Kepala.Width = 119;
            Kepala.Height = 5;
            Kepala.Tampil();

            Kotak kiri = new Kotak();
            kiri.X = 0;
            kiri.Y = 6;
            kiri.SetWidthAndHeight(30, 18);
            kiri.Tampil();

            Kotak Kanan = new Kotak();
            Kanan.X = 32;
            Kanan.Y = 6;
            Kanan.SetWidthAndHeight(87, 18);
            Kanan.Tampil();

            Kotak kaki = new Kotak();
            kaki.X = 0;
            kaki.Y = 25;
            kaki.Width = 119;
            kaki.Height = 3;
            kaki.Tampil();

            Menu menu = new Menu("JALANKAN", "LIHAT DATA", "TAMBAH DATA", "EDIT DATA", "HAPUS DATA", "KELUAR");
            menu.SetXY(5, 10);
            menu.ForeColor = ConsoleColor.Cyan;
            menu.SelectedBackColor = ConsoleColor.Yellow;
            menu.SelectedForeColor = ConsoleColor.Black;
            menu.Tampil();
            logo();

            bool IsiProgramJalan = true
                ;

            while (IsiProgramJalan)
            { 
                ConsoleKeyInfo Tombol = Console.ReadKey(true);

                if (Tombol.Key == ConsoleKey.DownArrow)
                {
                    //Tombol Panah Ke Bawah
                    menu.Next();
                    menu.Tampil();
                }
                else if (Tombol.Key == ConsoleKey.UpArrow)
                {
                    // Tombol Panah Ke Atas
                    menu.Prev();
                    menu.Tampil();
                }
                else if (Tombol.Key == ConsoleKey.Enter)
                {
                    // Enter 
                    int MenuTerpilih = menu.SelectedIndex;

                    if(MenuTerpilih == 0)
                    {
                        // Menu Jalankan
                        Jalankan();
                    }
                    else if(MenuTerpilih == 1)
                    {
                        // Menu Lihat Data
                       LihatData();
                    }
                    else if (MenuTerpilih == 2)
                    {
                        // Menu Tambah Data
                        TambahData();
                    }
                    else if (MenuTerpilih == 3)
                    {
                        // Menu Edit Data
                        EditData();
                    }
                    else if (MenuTerpilih == 4)
                    {
                        // Menu Hapus Data
                        HapusData();
                    }
                    else if(MenuTerpilih == 5)
                    {
                        // Keluar Aplikasi
                        IsiProgramJalan = false;
                    }
                }
                
 
            }


        }
        static void Jalankan()
        {
            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 7).SetText(".: JALANKAN :.").SetLength(79);
            Judul.TampilTengah();

            Tulisan HariSekarang = new Tulisan().SetXY(33, 9);
            Tulisan JamSekarang = new Tulisan().SetXY(33, 10);

            string QSelect = "SELECT * FROM tb_jadwal WHERE hari AND jam=@jam;";
            DB.Connect();

            bool Play = true;

            while (Play)
            {
                DateTime Sekarang = DateTime.Now;

                HariSekarang.SetText("HARI SEKARANG : " + Sekarang.ToString("dddd"));
                HariSekarang.Tampil();

                JamSekarang.SetText("JAM SEKARANG :" + Sekarang.ToString("HH:mm:ss"));
                JamSekarang.Tampil();

                DataTable DT = DB.RunQuery(QSelect,
                    new OleDbParameter("@hari", Sekarang.ToString("dddd")),
                    new OleDbParameter("@jam", Sekarang.ToString("HH:mm")));

                if(DT.Rows.Count > 0)
                {
                    Audio HAA = new Audio();
                    HAA.File = "./Suara/" + DT.Rows[0]["sound"];
                    HAA.Play();

                    new Tulisan().SetXY(31, 14).SetText("BEL TELAH BERBUNYI!!!").SetBackColor(ConsoleColor.Red).SetLength(79).Tampil();
                    new Tulisan().SetXY(31, 15).SetText(DT.Rows[0]["ket"].ToString()).SetBackColor(ConsoleColor.Red).SetLength(79).Tampil();

                    Play = false;

                }

                Thread.Sleep(1000);

            }

        }

        static void LihatData()
        {
            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 7).SetText(".:LIHAT DATA JADWAL :.").SetLength(79);
            Judul.TampilTengah();

            DB.Connect();
            DataTable DT = DB.RunQuery("SELECT * FROM tb_jadwal;");

            new Tulisan("┌───────────┬─────────────────┬──────────────────┬───────────────────────────────┐").SetXY(34, 10).Tampil();
            new Tulisan("│    ID     │      HARI       │       JAM        │           KETERANGAN          │").SetXY(34, 11).Tampil();
            new Tulisan("├───────────┼─────────────────┼──────────────────┼───────────────────────────────┤").SetXY(34, 12).Tampil();

            


            for (int i = 0; i < DT.Rows.Count; i++)
            {
                string ID = DT.Rows[i]["id"].ToString();
                string HARI = DT.Rows[i]["hari"].ToString();
                string JAM = DT.Rows[i]["jam"].ToString();
                string KETERANGAN = DT.Rows[i]["KETERANGAN"].ToString();

                string isi = string.Format("|{0,-11}|{1, -17}|{2, -18}|{3, -31}|", ID, HARI, JAM, KETERANGAN);
                new Tulisan(isi).SetXY(34, 13 + i).Tampil();
            }

            new Tulisan("└───────────┴─────────────────┴──────────────────┴───────────────────────────────┘").SetXY(34, 13 + DT.Rows.Count).Tampil();



        }
        static void TambahData()
        {
            new Clear(33, 7, 82, 16).Tampil();

            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 7).SetText(".:TAMBAH DATA JADWAL :.").SetLength(79);
            Judul.TampilTengah();

            Inputan HariInput = new Inputan();
            HariInput.Text = "Masukkan Hari :";
            HariInput.SetXY(33, 9);

            Inputan JamInput = new Inputan();
            JamInput.Text = "Masukkan Jam :";
            JamInput.SetXY(33, 11);

            Inputan KetInput = new Inputan();
            KetInput.Text = "Masukkan Keterangan :";
            KetInput.SetXY(33, 13);

            //Inputan SoundInput = new Inputan();
            //SoundInput.Text = "Masukkan Audio :";
            //SoundInput.SetXY(33,12);

            Pilihan SoundInput = new Pilihan();
            SoundInput.SetPilihans(
             "5 Menit Akhir Istirahat I.wav",
             "5 Menit Akhir Istirahat II.wav",
             "5 Menit Akhir Istirahat III.wav",
             "Pelajaran ke 1.wav",
             "Pelajaran ke 2.wav",
             "Pelajaran ke 3.wav",
             "Pelajaran ke 4.wav",
             "Pelajaran ke 5.wav",
             "Pelajaran ke 6.wav",
             "Pelajaran ke 7.wav",
             "Pelajaran ke 8.wav",
             "Pelajaran ke 9.wav",
             "pembuka.wav");

            SoundInput.Text = "Masukkan Audio :";
            SoundInput.SetXY(33, 15);

            string Hari = HariInput.Read();
            string Jam = JamInput.Read();
            string Ket = KetInput.Read();
            string Sound = SoundInput.Read();

            DB.Connect();
            DB.RunNonQuery("INSERT INTO tb_jadwal ( hari, jam , keterangan , sound ) VALUES (@hari, @jam, @keterangan, @sound);",
                new OleDbParameter("@hari", Hari),
                new OleDbParameter("@jam", Jam),
                new OleDbParameter("@ket", Ket),
                new OleDbParameter("@sound", Sound)
                );

            DB.Disconnect();
            new Tulisan().SetXY(31, 17).SetText("DATA Berhasil Di Simpan!!!").SetBackColor(ConsoleColor.Red).SetLength(79).TampilTengah();
            
        }

        static void EditData()
        {
            new Clear(33, 7, 78, 16).Tampil();

            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 7).SetText(".:LIHAT DATA JADWAL.:").SetLength(79);
            Judul.TampilTengah();

            Inputan IDInputDirubah = new Inputan();
            IDInputDirubah.Text = "Masukkan ID Jadwal Yang Ingin Di Rubah :";
            IDInputDirubah.SetXY(33, 9);

            Inputan HariInput = new Inputan();
            HariInput.Text = "Masukkan Hari :";
            HariInput.SetXY(33, 9);

            Inputan JamInput = new Inputan();
            JamInput.Text = "Masukkan Jam :";
            JamInput.SetXY(33, 11);

            Inputan KetInput = new Inputan();
            KetInput.Text = "Masukkan Keterangan :";
            KetInput.SetXY(33, 13);

            //Inputan SoundInput = new Inputan();
            //SoundInput.Text = "Masukkan Audio :";
            //SoundInput.SetXY(33,12);

            Pilihan SoundInput = new Pilihan();
            SoundInput.SetPilihans(
             "5 Menit Akhir Istirahat I.wav",
             "5 Menit Akhir Istirahat II.wav",
             "5 Menit Akhir Istirahat III.wav",
             "Pelajaran ke 1.wav",
             "Pelajaran ke 2.wav",
             "Pelajaran ke 3.wav",
             "Pelajaran ke 4.wav",
             "Pelajaran ke 5.wav",
             "Pelajaran ke 6.wav",
             "Pelajaran ke 7.wav",
             "Pelajaran ke 8.wav",
             "Pelajaran ke 9.wav",
             "pembuka.wav");

            SoundInput.Text = "Masukkan Audio :";
            SoundInput.SetXY(33, 14);

            string IDRubah = IDInputDirubah.Read();
            string Hari = HariInput.Read();
            string Jam = JamInput.Read();
            string ket = KetInput.Read();
            string Sound = SoundInput.Read();

            DB.Connect();
            DB.RunNonQuery("INSERT INTO tb_jadwal ( hari, jam , ket , sound ) VALUES ( @hari, @jam, @ket,@sound );",
                new OleDbParameter("@hari", Hari),
                new OleDbParameter("@jam", Jam),
                new OleDbParameter("@ket", ket),
                new OleDbParameter("@sound", Sound)
                );

            DB.Disconnect();
            new Tulisan().SetXY(31, 14).SetText("DATA Berhasil Di Update!!!").SetBackColor(ConsoleColor.Red).SetLength(79).TampilTengah();
        }

        static void HapusData()
        {
            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 7).SetText(".:HAPUS DATA JADWAL :.").SetLength(79);
            Judul.TampilTengah();

            Inputan IDInput = new Inputan();
            IDInput.Text = "Masukkan ID Yang Akan Di Hapus :";
            IDInput.SetXY(33, 9);
            string ID = IDInput.Read();

            DB.Connect();
            DB.RunNonQuery("DELETE FROM tb_jadwal WHERE id=@id;",
                new OleDbParameter("@id", ID));
            DB.Disconnect();

            new Tulisan().SetXY(31, 12).SetText("Data Berhasil Di Hapus!!!").SetBackColor(ConsoleColor.Red).SetLength(79).TampilTengah();

        }

        static void logo()
        {
            new Clear(33, 7, 86, 16).SetBackColor(ConsoleColor.DarkCyan).Tampil();
            new Tulisan("                        .               ").SetXY(31, 7).SetLength(79).SetForeColor(ConsoleColor.Black).SetBackColor(ConsoleColor.DarkCyan).TampilTengah();
            new Tulisan("                    :-=++:              ").SetXY(31, 8).SetLength(79).SetForeColor(ConsoleColor.Black).SetBackColor(ConsoleColor.DarkCyan).TampilTengah();
            new Tulisan("               .:-+++++++.              ").SetXY(31, 9).SetLength(79).SetForeColor(ConsoleColor.Black).SetBackColor(ConsoleColor.DarkCyan).TampilTengah();
            new Tulisan("       -=++++++++++++++++++++++=-.      ").SetXY(31, 10).SetLength(79).SetForeColor(ConsoleColor.Black).SetBackColor(ConsoleColor.DarkCyan).TampilTengah();
            new Tulisan("     :++++++++++++++++++++++++++++-     ").SetXY(31, 11).SetLength(79).SetForeColor(ConsoleColor.Black).SetBackColor(ConsoleColor.DarkCyan).TampilTengah();
            new Tulisan("    .++++++++++++++++++++++++++++++:    ").SetXY(31, 12).SetLength(79).SetForeColor(ConsoleColor.Black).SetBackColor(ConsoleColor.DarkCyan).TampilTengah();
            new Tulisan("    .+++++++++-:-+++++++-.=++++++++-    ").SetXY(31, 13).SetLength(79).SetForeColor(ConsoleColor.Black).SetBackColor(ConsoleColor.DarkCyan).TampilTengah();
            new Tulisan("    .+++++++++++++=-==+++++++++++++-    ").SetXY(31, 15).SetLength(79).SetForeColor(ConsoleColor.Black).SetBackColor(ConsoleColor.DarkCyan).TampilTengah();
            new Tulisan("    .+++++++++++++-----==++++++++++-   ").SetXY(31, 16).SetLength(79).SetForeColor(ConsoleColor.Black).SetBackColor(ConsoleColor.DarkCyan).TampilTengah();
            new Tulisan("    .+++++++++++++-----==++++++++++-    ").SetXY(31, 17).SetLength(79).SetForeColor(ConsoleColor.Black).SetBackColor(ConsoleColor.DarkCyan).TampilTengah();
            new Tulisan("    .+++++++++++++-----==++++++++++-    ").SetXY(31, 18).SetLength(79).SetForeColor(ConsoleColor.Black).SetBackColor(ConsoleColor.DarkCyan).TampilTengah();
            new Tulisan("     ++++++++++++++++++++++++++++++:    ").SetXY(31, 19).SetLength(79).SetForeColor(ConsoleColor.Black).SetBackColor(ConsoleColor.DarkCyan).TampilTengah();
            new Tulisan("     .++++++++++++++++++++++++++++-     ").SetXY(31, 20).SetLength(79).SetForeColor(ConsoleColor.Black).SetBackColor(ConsoleColor.DarkCyan).TampilTengah();
            new Tulisan("       :==+++++++++++++++++++++=:       ").SetXY(31, 21).SetLength(79).SetForeColor(ConsoleColor.Black).SetBackColor(ConsoleColor.DarkCyan).TampilTengah();


        }

    }

}
;