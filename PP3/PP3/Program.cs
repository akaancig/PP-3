///
/// Name -> AHMET KAAN CIG
/// ID -> 170101003
///
using System;
using System.Collections.Generic;
using System.Threading;

namespace PP3
{
    // Zaman degerlerini tutmak icin her dugum icin tanımlanmis struct yapisi( ORNEGIN = [1,0,0] )
    public struct nodes
    {
        public int t1, t2, t3;
    }
    class Program
    {
        // Prosesimizin nodelarinin tutulacagi list
        public static List<nodes> currentProcess = new List<nodes>();
        public static int numOfProcesses = 3;
        public static int sayac = 0;
        public static int simProcess = 0; // simulate edilecek proses numarasi.
        public static void Main(string[] args)
        {
            // Burada process sayisinin dinamik olarak belirlenmesi amaclandi ancak bu kod satiri saglikli calismadigi icin daha sonra sabit deger olarak belirlendi
            /*
            while (numOfProcesses < 1 || numOfProcesses > 3)
            {
                try
                {
                    Console.Write("Proses sayisi giriniz: ");
                    numOfProcesses = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("");

                }
                catch (Exception)
                {
                    numOfProcesses = 0;
                }

            }
            */
            // Burada simule edilecek proses numarasi kullanicidan alinir...
            while (simProcess < 1 || simProcess > 3)
            {
                try
                {
                    Console.Write("Simule edilecek prosesi giriniz: ");
                    simProcess = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("");

                }
                catch (Exception)
                {
                    simProcess = 0;
                }

            }
            int choice = 0;
            createNode(); // Ilk dugum 0,0,0 degeri kodun saglikli calismasi icin burada oluştrulur...
            // Menu icin kullanilan do-while loop...
            do
            {
                Console.WriteLine("1- Mesaj Gonder");
                Console.WriteLine("2- Mesaj Alindisi Ilet");
                Console.WriteLine("3- En Son Sekliyle Zaman Çizgilerini Görüntüle");
                Console.WriteLine("4- Cikis\n");
                Console.Write("Secim: ");
                choice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("");
                if (choice == 1)
                {
                    sendMessage();
                }
                else if (choice == 2)
                {
                    receiveMsg();
                }
                else if (choice == 3)
                {
                    ShowTimeLine();
                }
                else if (choice == 4)
                {
                    ShowTimeLine();
                }
                // Her islemdene  sonra eger o anki girilen islem 1'den buyuk ise queueAyar fonksiyonu queue ve deliver islemleri icin ayarlamaları gerceklestirir.
                if(currentProcess.Count >1)
                    queueAyar();

            } while (choice != 4);

        }
        // Yeni bir node olusturur ve ilk degerleri her zaman sifirdir...
        public static void createNode()
        {
            nodes cacheN;
            cacheN.t1 = 0;
            cacheN.t2 = 0;
            cacheN.t3 = 0;
            currentProcess.Add(cacheN);
        }
        // O ana kadar girilmis olan degerlere gore ornek ciktiyi ekrana basar.
        public static void ShowTimeLine()
        {
            for (int i = 0;i< sayac; i++)
            {
                Console.WriteLine(" t" + (i + 1).ToString() + "-> " + "[" + currentProcess[i].t1.ToString() + "," + currentProcess[i].t2.ToString() + "," + currentProcess[i].t3.ToString() + "]");
            }
            Console.WriteLine("");
        }
        // Menuden mesaj gonder secenegiyle gidilir.
        // Herhangi bir prosese mesaj gondermek icin kullanilir.
        public static void sendMessage()
        {
            // eger ilk adimda degilsek mesaj gonderimi dolayısıyla listeye yeni bir dugum eklenir. sayac != 0
            // Ilk adım ise zaten olusturulmus olan mesaj kullanılarak (bir artırılarak) islem yapilir.. sayac == 0
            if(sayac != 0)
                createNode();
            Console.Write("Mesajinizi giriniz: ");
            string msg = Console.ReadLine();
            // Islem yaptigimiz proses 1,2 yada 3 ise buna gore mesaj gonderimi sonrasi o prosese ait artırılması gereken deger hangisi ise o deger artırılır.
            if (simProcess == 1)
            {
                if (sayac > 0)
                {
                    nodes cacheN;
                    cacheN.t1 = currentProcess[sayac - 1].t1+1; // Ilk Process ise t1 burada artırılır.(Onceki degere sayac-1 ile gidilip sonra o degere 1 eklenir...)
                    cacheN.t2 = currentProcess[sayac - 1].t2;
                    cacheN.t3 = currentProcess[sayac - 1].t3;
                    currentProcess[sayac] = cacheN;
                }
                // Eger ilk node ise burada islem yapilarak artirilma yapilir...
                else
                {
                    nodes cacheN;
                    cacheN.t1 = currentProcess[sayac].t1+1;
                    cacheN.t2 = currentProcess[sayac].t2;
                    cacheN.t3 = currentProcess[sayac].t3;
                    currentProcess[sayac] = cacheN;
                }

            }
            else if (simProcess == 2)
            {
                if(sayac > 0)
                {
                    nodes cacheN;
                    cacheN.t1 = currentProcess[sayac - 1].t1;
                    cacheN.t2 = currentProcess[sayac - 1].t2 + 1;// Ikıncı Process ise t2 burada artırılır.(Onceki degere sayac-1 ile gidilip sonra o degere 1 eklenir...)
                    cacheN.t3 = currentProcess[sayac - 1].t3;
                    currentProcess[sayac] = cacheN;
                }
                // Eger ilk node ise burada islem yapilarak artirilma yapilir...
                else
                {
                    nodes cacheN;
                    cacheN.t1 = currentProcess[sayac].t1;
                    cacheN.t2 = currentProcess[sayac].t2 + 1;
                    cacheN.t3 = currentProcess[sayac].t3;
                    currentProcess[sayac] = cacheN;
                }
                
            }
            else if (simProcess == 3)
            {
                if (sayac > 0)
                {
                    nodes cacheN;
                    cacheN.t1 = currentProcess[sayac - 1].t1;
                    cacheN.t2 = currentProcess[sayac - 1].t2;
                    cacheN.t3 = currentProcess[sayac - 1].t3+1;// Ucuncu Process ise t3 burada artırılır.(Onceki degere sayac-1 ile gidilip sonra o degere 1 eklenir...)
                    currentProcess[sayac] = cacheN;
                }
                // Eger ilk node ise burada islem yapilarak artirilma yapilir...
                else
                {
                    nodes cacheN;
                    cacheN.t1 = currentProcess[sayac].t1;
                    cacheN.t2 = currentProcess[sayac].t2;
                    cacheN.t3 = currentProcess[sayac].t3+1;
                    currentProcess[sayac] = cacheN;
                }
            }
            Console.WriteLine(msg + " mesaji tum nodelara gonderildi. Timestamp: [" + currentProcess[sayac].t1.ToString()+","+currentProcess[sayac].t2.ToString() + ","+ currentProcess[sayac].t3.ToString() + "]");
            sayac++;
        }
        // Mesaj alindisi iletmek icin bu fonksiyon kullanilir...
        public static void receiveMsg()
        {
            // eger ilk adimda degilsek mesaj gonderimi dolayısıyla listeye yeni bir dugum eklenir. sayac != 0
            if (sayac != 0)
                createNode();
            Console.Write("Mesajinizi giriniz: ");
            string msg = Console.ReadLine();
            Console.Write("Mesaji gönderen process: ");
            int gondericiProcess = Convert.ToInt32(Console.ReadLine());
            Console.Write("Timestamp giriniz(ex:1,0,0): ");
            string timestamp = Console.ReadLine();
            string[] values = timestamp.Split(',');// Timestamp degerini virgule gore boler...
            nodes cacheN;
            cacheN.t1 = Convert.ToInt32(values[0]);
            cacheN.t2 = Convert.ToInt32(values[1]); 
            cacheN.t3 = Convert.ToInt32(values[2]);
            currentProcess[sayac] = cacheN; // Alindi sirasında gelen deger yeni olusturulan node'a yazilir...
            Console.WriteLine(msg + "mesaji alındi. Timestamp: [" + cacheN.t1.ToString() + "," + cacheN.t2.ToString() + "," + cacheN.t3.ToString() + "]");
            sayac++;
        }
        // Bu fonksiyon her bir islemden sonra cagirilarak queue,recieve,deliver olaylarini yuruten fonksiyondur.
        public static void queueAyar()
        {
            try
            {
                // queue için tutulan list degeridir.
                List<nodes> queuee = new List<nodes>();
                int flag = 1;
                int i = 0, j = 0;
                // Suanki guncel olaydan sonra yeni olusan currentProcess  queue list'ine  atılır...
                for (int k = 0; k < sayac; k++)
                    queuee.Add(currentProcess[k]);
                while (flag == 1)
                {
                    int flag2 = 0;
                    for (i = 0; i < sayac; i++)
                    {
                        float dst1 = (float)Math.Sqrt(queuee[i].t1 * queuee[i].t1 + queuee[i].t2 * queuee[i].t2 + queuee[i].t3 * queuee[i].t3);
                        for (j = i; j < sayac; j++)
                        {
                            // Her bir deger icin digerleriyle buyukluk kucukluk kontrolu burada yapilir eger deger uygunsa icine yazilir...
                            float dst2 = (float)Math.Sqrt(queuee[j].t1 * queuee[j].t1 + queuee[j].t2 * queuee[j].t2 + queuee[j].t3 * queuee[j].t3);
                            if ((queuee[j].t1 < queuee[i].t1 || queuee[j].t1 == queuee[i].t1) && (queuee[j].t2 == queuee[i].t2 || queuee[j].t2 < queuee[i].t2) && (queuee[j].t3 == queuee[i].t3 || queuee[j].t3 < queuee[i].t3))
                            {
                                if (dst1 > dst2)
                                {
                                    flag2 = 1;
                                    flag = 1;
                                    break;
                                }
                            }
                        }
                        if (flag2 == 1 && i != sayac && j != sayac)
                        {
                            nodes cache;
                            cache.t1 = queuee[i].t1;
                            cache.t2 = queuee[i].t2;
                            cache.t3 = queuee[i].t3;
                            queuee[i] = queuee[j];
                            queuee[j] = cache;
                        }
                    }
                    flag = 0;
                }
                // Burada guncel olarak hesaplanmis degerler currentProcess'e atilarak guncellenir...
                currentProcess = queuee;
            }
            catch{ }           
        }
    }
}
