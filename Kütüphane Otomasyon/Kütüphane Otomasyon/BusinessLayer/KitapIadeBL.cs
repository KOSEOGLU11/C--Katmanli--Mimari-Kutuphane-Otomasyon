﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DAL;      // DAL katmanına erişim sağlandı
//using Entity;  //   Entity katmanına erişim sağlandı
using Kütüphane_Otomasyon.DataAccessLayer;
using Kütüphane_Otomasyon.Entity;

namespace BL
{
    public class KitapIadeBL
    {

        // DAL katmanı kullanılarak kitap id ye ait ogrenci verileri çekildi
        public static List<Kitap_Ogrenci> kitapOgrenciListe(Kitap_Ogrenci kitap)
        {
            if (kitap.KitapId != 0)
                return KitapIadeDAL.kitapOgrenciListe(kitap);
            else
                return null;
        }


        //Ogrencinin aldiği kitaplar DAL katmanı kullanılarak listelendi
        public static List<string> kitapAlinanLİste(Kitap_Iade kitap)
        {

            return KitapIadeDAL.kitapAlinanListe(kitap);
        }


        //Ogrencinin teslim etmesi gereken kitaplar DAL katmanı kullanılarak listelendi
        public static List<string> kitapTeslimLİste(Kitap_Iade kitap)
        {

            return KitapIadeDAL.kitapTeslimListe(kitap);
        }

        //kitabın Alinma tarihini DAL katmanı kullanarak öğrendik
        public static List<string> kitapTarih(Kitap_Iade kitap)
        {

            return KitapIadeDAL.kitapTarih(kitap);
        }

        // Ogrenciye ait ceza bilgisini  DAL katmanını kullanrak öğrendik
        public static List<string> ogrenciCeza(Kitap_Iade kitap)
        {

            return KitapIadeDAL.OgrenciCeza(kitap);
        }


        //Öğrenciye ait güncel ceza DAL katmanı kullanılarak eklendi
        public static int ogrenciCezaIslemi(Kitap_Iade ogrenci)
        {
            if (ogrenci.OgrenciCeza >= 0)
            {

                return KitapIadeDAL.ogrenciCezaIslemi(ogrenci);
            }

            else
                return -1;
        }


        // öğrenciye ait alinabilir kitaplarin sayisi DAL katmanı kullanılarak int döndürüldü
        public static int grafikAlinabilir(Kitap_Iade kitap)
        {
            if (kitap.OgrenciId != 0)
            {
                return KitapIadeDAL.grafikAlinabilir(kitap);
            }
            return 0;
        }

        // öğrenciye ait almış olduğu kitaplarin sayisi DAL katmanı kullanılarak int döndürüldü
        public static int grafikVerilmis(Kitap_Iade kitap)
        {
            if (kitap.OgrenciId != 0)
            {
                return KitapIadeDAL.grafikVerilmis(kitap);
            }
            return 0;
        }

        // Tüm kitapların sayisi DAL katmanı kullanılarak int döndürüldü
        public static int grafikHepsi()
        {
            return KitapIadeDAL.grafikHepsi();
        }


        // Id ile öğrencini almış olduğu kitaplar tarihsel bir şekilde  DAL katmanı kullanılarak listelendi
        public static List<AlınanKitap> ogrenciIdListe(AlınanKitap kitap)
        {

            return KitapIadeDAL.ogrenciIdListe(kitap);
        }

        //Kİtap alma işlemi DAL katmanı kullanılarak gerçekleştirildi
        public static int kitapAlimIslemi(Kitap_Iade kitap)
        {
            if (kitap.KitapId != 0 && kitap.OgrenciId != 0 && kitap.KitapAlinma != null)
            {

                return KitapIadeDAL.kitapAlimIslemi(kitap);
            }

            else
                return -1;
        }

        //Kitap teslim etme işlemi DAL katmanı kullanılarak gerçekleştirildi
        public static int kitapTeslimIslemi(Kitap_Iade kitap)
        {
            if (kitap.KitapId != 0)
            {

                return KitapIadeDAL.kitapTeslimIslemi(kitap);
            }

            else
                return -1;
        }



        // Kitap adı ile kitap Id bilgisi DAL katmanı kullanılarak int döndürüldü
        public static int kitapId(Kitap_Ogrenci kitap)
        {
            if (kitap.KitapAd != "")
            {

                return KitapIadeDAL.kitapId(kitap);
            }

            else
                return -1;
        }





    }


}
