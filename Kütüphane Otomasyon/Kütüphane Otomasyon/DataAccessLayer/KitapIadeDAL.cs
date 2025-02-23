﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;  // Veri tabanı bağlantısı için gerekli kütüphaneyi ekledik.
using System.Data;       // Veri tabanı bağlantısı için gerekli kütüphaneyi ekledik.
//using Entity;           // Entity katmanını kullandık.
using Kütüphane_Otomasyon.Entity;

namespace DAL
{
    public class KitapIadeDAL
    {
        //Kitabı alanların detaylı listesini inner join yöntemi ile 3 tablodan verilerini çekerek aldık
        public static List<Kitap_Ogrenci> kitapOgrenciListe(Kitap_Ogrenci kitap)
        {


            OleDbCommand command = new OleDbCommand("Select k.KitapId,o.OgrenciAd,o.OgrenciSoyad,k.KitapAd,kk.KitapAlinma,kk.KitapTeslim,kk.KitapKontrol from ((Kitap k inner join KitapKayit kk on k.KitapId=kk.KitapId) inner join Ogrenci o on o.OgrenciId=kk.OgrenciId) where k.KitapId = @KitapId", Baglanti.baglanti);
            Baglanti.Connection(command);
            command.Parameters.AddWithValue("@KitapId", kitap.KitapId);
            OleDbDataReader read = command.ExecuteReader();
            List<Kitap_Ogrenci> kitapOgrenci = new List<Kitap_Ogrenci>();

            while (read.Read())
            {
                kitapOgrenci.Add(new Kitap_Ogrenci
                {
                    KitapId = int.Parse(read["KitapId"].ToString()),
                    OgrenciAd = read["OgrenciAd"].ToString(),
                    OgrenciSoyad = read["OgrenciSoyad"].ToString(),
                    KitapAd = read["KitapAd"].ToString(),
                    KitapAlinma = DateTime.Parse(read["KitapAlinma"].ToString()),
                    KitapTeslim = read["KitapTeslim"].ToString(),
                    KitapKontrol = bool.Parse(read["KitapKontrol"].ToString())
                });
            }

            return kitapOgrenci;
        }


        //Ogrencinin almış olduğu kitaplar tarihsel olarak listelendi. Yine inner join yöntemi ile veriler çekildi
        public static List<AlınanKitap> ogrenciIdListe(AlınanKitap kitap)
        {


            OleDbCommand command = new OleDbCommand("Select o.OgrenciId,k.KitapAd,kk.KitapAlinma,kk.KitapTeslim,kk.KitapKontrol from ((Kitap k inner join KitapKayit kk on k.KitapId=kk.KitapId) inner join Ogrenci o on o.OgrenciId=kk.OgrenciId) where o.OgrenciId = @OgrenciId", Baglanti.baglanti);
            Baglanti.Connection(command);
            command.Parameters.AddWithValue("@OgrenciId", kitap.OgrenciId);
            OleDbDataReader read = command.ExecuteReader();
            List<AlınanKitap> kitapOgrenci = new List<AlınanKitap>();
            while (read.Read())
            {
                kitapOgrenci.Add(new AlınanKitap
                {
                    OgrenciId = int.Parse(read["OgrenciId"].ToString()),
                    KitapAd = read["KitapAd"].ToString(),
                    KitapAlinma = DateTime.Parse(read["KitapAlinma"].ToString()),
                    KitapTeslim = read["KitapTeslim"].ToString(),
                    KitapKontrol = bool.Parse(read["KitapKontrol"].ToString())
                });
            }

            return kitapOgrenci;
        }

        //Alinabilir durumdaki kitapların teslim edilme durumlarına göre listeleme işlemi gerçekleştirildi
        public static List<string> kitapAlinanListe(Kitap_Iade kitap)
        {
            OleDbCommand command = new OleDbCommand("Select KitapAd from Kitap where KitapId not in(select KitapId from KitapKayit where OgrenciId=@OgrenciId and KitapKontrol=false)", Baglanti.baglanti);
            Baglanti.Connection(command);
            command.Parameters.AddWithValue("@OgrenciId", kitap.OgrenciId);
            OleDbDataReader read = command.ExecuteReader();

            List<string> liste = new List<string>();

            while (read.Read())
            {
                liste.Add(read["KitapAd"].ToString());
            }

            return liste;
        }

        //Teslim edilmesi gereken kitaplar teslim edilmeme durumuna göre listeye aktarıldı
        public static List<string> kitapTeslimListe(Kitap_Iade kitap)
        {
            OleDbCommand command = new OleDbCommand("Select KitapAd from Kitap where KitapId in(select KitapId from KitapKayit where OgrenciId=@OgrenciId and KitapKontrol=false)", Baglanti.baglanti);
            Baglanti.Connection(command);
            command.Parameters.AddWithValue("@OgrenciId", kitap.OgrenciId);
            OleDbDataReader read = command.ExecuteReader();

            List<string> liste = new List<string>();

            while (read.Read())
            {
                liste.Add(read["KitapAd"].ToString());
            }

            return liste;
        }

        //Öğrenciye verilmiş kitapların sayısı int olarak döndürüldü
        public static int grafikVerilmis(Kitap_Iade kitap)
        {
            OleDbCommand command = new OleDbCommand("Select KitapAd from Kitap where KitapId in(select KitapId from KitapKayit where OgrenciId=@OgrenciId and KitapKontrol=false)", Baglanti.baglanti);
            Baglanti.Connection(command);
            command.Parameters.AddWithValue("@OgrenciId", kitap.OgrenciId);
            OleDbDataReader read = command.ExecuteReader();
            int sayac = 0;
            while (read.Read())
            {
                sayac++;
            }

            return sayac;
        }

        //Tüm kitapların sayısı int olarak döndürüldü
        public static int grafikHepsi()
        {
            OleDbCommand command = new OleDbCommand("Select * from Kitap", Baglanti.baglanti);
            Baglanti.Connection(command);
            OleDbDataReader read = command.ExecuteReader();
            int sayac = 0;
            while (read.Read())
            {
                sayac++;
            }

            return sayac;
        }

        //Alınabilir kitapların sayısı int olarak döndürüldü
        public static int grafikAlinabilir(Kitap_Iade kitap)
        {
            OleDbCommand command = new OleDbCommand("Select KitapAd from Kitap where KitapId not in(select KitapId from KitapKayit where OgrenciId=@OgrenciId and KitapKontrol=false)", Baglanti.baglanti);
            Baglanti.Connection(command);
            command.Parameters.AddWithValue("@OgrenciId", kitap.OgrenciId);
            OleDbDataReader read = command.ExecuteReader();
            int sayac = 0;
            while (read.Read())
            {
                sayac++;
            }

            return sayac;
        }

        // Alınma tarihini kitapkayıt tablosundan çekip listeye aktardık
        public static List<string> kitapTarih(Kitap_Iade kitap)
        {
            OleDbCommand command = new OleDbCommand("Select KitapAlinma from KitapKayit where KitapId=@KitapId and OgrenciId=@OgrenciId", Baglanti.baglanti);
            Baglanti.Connection(command);
            command.Parameters.AddWithValue("@KitapId", kitap.KitapId);
            command.Parameters.AddWithValue("@OgrenciId", kitap.OgrenciId);
            OleDbDataReader read = command.ExecuteReader();

            List<string> liste = new List<string>();

            while (read.Read())
            {
                kitap.KitapAlinma = DateTime.Parse(read["KitapAlinma"].ToString());
            }

            return liste;
        }


        //Veritabanından öğrenciye ait ceza bilgisi listeye aktarıldı
        public static List<string> OgrenciCeza(Kitap_Iade kitap)
        {
            OleDbCommand command = new OleDbCommand("Select o.OgrenciCeza from ((Kitap k inner join KitapKayit kk on k.KitapId=kk.KitapId) inner join Ogrenci o on o.OgrenciId=kk.OgrenciId) where o.OgrenciId = @OgrenciId", Baglanti.baglanti);
            Baglanti.Connection(command);
            command.Parameters.AddWithValue("@OgrenciId", kitap.OgrenciId);
            OleDbDataReader read = command.ExecuteReader();

            List<string> liste = new List<string>();

            while (read.Read())
            {
                kitap.OgrenciCeza = float.Parse(read["OgrenciCeza"].ToString());
            }

            return liste;
        }

        //Ceza işlemi gerçekleştirildikten sonra ogrenciye ait ceza verisinde guncelleme yapıldı
        public static int ogrenciCezaIslemi(Kitap_Iade ogrenci)
        {
            OleDbCommand command = new OleDbCommand("Update Ogrenci set OgrenciCeza=@OgrenciCeza where OgrenciId=@OgrenciId", Baglanti.baglanti);
            Baglanti.Connection(command);

            command.Parameters.AddWithValue("@OgrenciCeza", ogrenci.OgrenciCeza);
            command.Parameters.AddWithValue("@OgrenciId", ogrenci.OgrenciId);

            return command.ExecuteNonQuery();
        }






        //Kitap alındğında veri tabanına veriler eklendi
        public static int kitapAlimIslemi(Kitap_Iade kitap)
        {
            OleDbCommand command = new OleDbCommand("insert into  KitapKayit(KitapId,OgrenciId,KitapAlinma) values(@KitapId,OgrenciId,@KitapAlinma)", Baglanti.baglanti);
            Baglanti.Connection(command);
            command.Parameters.AddWithValue("@KitapId", kitap.KitapId);
            command.Parameters.AddWithValue("@OgrenciId", kitap.OgrenciId);
            command.Parameters.AddWithValue("@KitapAlinma", kitap.KitapAlinma);
            return command.ExecuteNonQuery();
        }

        //Kitap teslim edildiğinde veriler güncellendi
        public static int kitapTeslimIslemi()
        {
            OleDbCommand command = new OleDbCommand("Update KitapKayit set KitapTeslim=@KitapTeslim,KitapKontrol=@KitapKontrol where KitapId=@KitapId  and  OgrenciId=@Ogrenci", Baglanti.baglanti);
            Baglanti.Connection(command);
            command.Parameters.AddWithValue("@KitapTeslim", kitap.KitapTeslim);
            command.Parameters.AddWithValue("@KitapKontrol", kitap.KitapKontrol);
            command.Parameters.AddWithValue("@KitapId", kitap.KitapId);
            command.Parameters.AddWithValue("@OgrenciId", kitap.OgrenciId);
            return command.ExecuteNonQuery();
        }


        //Kitap adı sorgusu ile ada ait kitapId verisi int döndürüldü
        //public static int kitapId(Kitap_Ogrenci kitap)
        //{
        //    OleDbCommand command = new OleDbCommand("Select KitapId from Kitap where KitapAd=@KitapAd", Baglanti.baglanti);
        //    Baglanti.Connection(command);
        //    command.Parameters.AddWithValue("@KitapAd", kitap.KitapAd);
        //    OleDbDataReader read = command.ExecuteReader();

        //    int id = 0;
        //    while (read.Read())
        //    {

        //        id = int.Parse(read["KitapId"].ToString());
        //    }

        //    return id;


        }
    }
}
