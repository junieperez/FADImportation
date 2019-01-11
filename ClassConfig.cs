using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FAD_Importation.CLASSES
{
    public class ClassConfig
    {
        public static ClassConfig Instance { get { return Nested.Instance; } }

        private class Nested
        {
            static Nested() { }
            internal static readonly ClassConfig Instance = new ClassConfig();
        }

        private ClassConfig()
        {
            configs = new Dictionary<string, string>();
            //Database

            //Alta Citta Tagbilaran City Config
            configs.Add("AC_DATA", @"\\172.16.161.35\fad_stores\FAD_STORE_DATABASE\AC_DATA\database\");
            configs.Add("AC_MASTERFILE", @"\\172.16.161.35\fad_stores\FAD_MASTERFILE\masterfile\");
            configs.Add("AC_CNVPICT", @"\\172.16.161.35\fad_stores\FAD_STORE_DATABASE\AC_DATA\CNVPICT\");
            configs.Add("AC_SERVER", "172.16.161.37");
            configs.Add("AC_DOMAIN", "server161-35");
            configs.Add("AC_USER", "public");
            configs.Add("AC_PASSWORD", "public");
            configs.Add("AC_DBNAME", "db_importation");
            configs.Add("AC_DBUSER", "db_fad_junie");
            configs.Add("AC_DBPASS", "pass");
            configs.Add("AC_SELLOCCODE", "AC");

            //Alturas Mall Tagbilaran City Config
            configs.Add("AM_DATA", @"\\172.16.161.35\fad_stores\FAD_STORE_DATABASE\AM_DATA\database\");
            configs.Add("AM_MASTERFILE", @"\\172.16.161.35\fad_stores\FAD_MASTERFILE\masterfile\");
            configs.Add("AM_CNVPICT", @"\\172.16.161.35\fad_stores\FAD_STORE_DATABASE\AM_DATA\CNVPICT\");
            configs.Add("AM_SERVER", "172.16.161.37");
            configs.Add("AM_DOMAIN", "server161-35");
            configs.Add("AM_USER", "public");
            configs.Add("AM_PASSWORD", "public");
            configs.Add("AM_DBNAME", "db_importation");
            configs.Add("AM_DBUSER", "db_fad_junie");
            configs.Add("AM_DBPASS", "pass");
            configs.Add("AM_SELLOCCODE", "AM");

            //Alturas Supermarket Tubigon Config
            configs.Add("ASCTU_DATA", @"\\172.16.221.2\fad_data\database\");
            configs.Add("ASCTU_CNVPICT", @"\\172.16.221.2\fad_data\CNVPICT\");
            configs.Add("ASCTU_MASTERFILE", @"\\172.16.161.35\fad_stores\FAD_MASTERFILE\masterfile\");
            configs.Add("ASCTU_SERVER", "172.16.161.37");
            configs.Add("ASCTU_DOMAIN", "");
            configs.Add("ASCTU_USER", "public");
            configs.Add("ASCTU_PASSWORD", "public");
            configs.Add("ASCTU_DBNAME", "db_importation");
            configs.Add("ASCTU_DBUSER", "db_fad_junie");
            configs.Add("ASCTU_DBPASS", "pass");
            configs.Add("ASCTU_SELLOCCODE", "ASCTU");

            //Alturas Supermarket Talibon Config
            configs.Add("ASCT_DATA", @"\\172.16.22.1\programs\FAD_DATA\TAL_DATA\database\");
            configs.Add("ASCT_CNVPICT", @"\\172.16.22.1\programs\FAD_DATA\TAL_DATA\CNVPICT\");
            configs.Add("ASCT_MASTERFILE", @"\\172.16.161.35\fad_stores\FAD_MASTERFILE\masterfile\");
            configs.Add("ASCT_SERVER", "172.16.161.37");
            configs.Add("ASCT_DOMAIN", "server161-35");
            configs.Add("ASCT_USER", "public");
            configs.Add("ASCT_PASSWORD", "public");
            configs.Add("ASCT_DBNAME", "db_importation");
            configs.Add("ASCT_DBUSER", "db_fad_junie");
            configs.Add("ASCT_DBPASS", "pass");
            configs.Add("ASCT_SELLOCCODE", "ASCT");

            //BAMDECORP Config
            configs.Add("BAMDE_DATA", @"\\172.16.217.215\fad_data\database\");
            configs.Add("BAMDE_CNVPICT", @"\\172.16.217.215\fad_data\CNVPICT\");
            configs.Add("BAMDE_MASTERFILE", @"\\172.16.161.35\fad_stores\FAD_MASTERFILE\masterfile\");
            configs.Add("BAMDE_SERVER", "172.16.161.37");
            configs.Add("BAMDE_DOMAIN", "");
            configs.Add("BAMDE_USER", "public");
            configs.Add("BAMDE_PASSWORD", "public");
            configs.Add("BAMDE_DBNAME", "db_importation");
            configs.Add("BAMDE_DBUSER", "db_fad_junie");
            configs.Add("BAMDE_DBPASS", "pass");
            configs.Add("BAMDE_SELLOCCODE", "BAMDE");

            //Copra Buying Station Config
            configs.Add("CBS_DATA", @"\\172.16.20.1\fad_ubay\Database\database\");
            configs.Add("CBS_CNVPICT", @"\\172.16.20.1\fad_ubay\Database\CNVPICT\");
            configs.Add("CBS_MASTERFILE", @"\\172.16.161.35\fad_stores\FAD_MASTERFILE\masterfile\");
            configs.Add("CBS_SERVER", "172.16.161.37");
            configs.Add("CBS_DOMAIN", "server161-35");
            configs.Add("CBS_USER", "public");
            configs.Add("CBS_PASSWORD", "public");
            configs.Add("CBS_DBNAME", "db_importation");
            configs.Add("CBS_DBUSER", "db_fad_junie");
            configs.Add("CBS_DBPASS", "pass");
            configs.Add("CBS_SELLOCCODE", "CBS");

            //Central Distribution Center Config
            configs.Add("CDC_DATA", @"\\172.16.192.51\cdc_fad\databases\");
            configs.Add("CDC_CNVPICT", @"\\172.16.192.51\cdc_fad\CNVPICT\");
            configs.Add("CDC_MASTERFILE", @"\\172.16.161.35\fad_stores\FAD_MASTERFILE\masterfile\");
            configs.Add("CDC_SERVER", "172.16.161.37");
            configs.Add("CDC", "");
            configs.Add("CDC_USER", "public");
            configs.Add("CDC_PASSWORD", "public123");
            configs.Add("CDC_DBNAME", "db_importation");
            configs.Add("CDC_DBUSER", "db_fad_junie");
            configs.Add("CDC_DBPASS", "pass");
            configs.Add("CDC_SELLOCCODE", "CDC");

            //Demo Farms Config
            configs.Add("DF_DATA", @"\\172.16.20.1\fad_ubay\Database\database\");
            configs.Add("DF_CNVPICT", @"\\172.16.20.1\fad_ubay\Database\CNVPICT\");
            configs.Add("DF_MASTERFILE", @"\\172.16.161.35\fad_stores\FAD_MASTERFILE\masterfile\");
            configs.Add("DF_SERVER", "172.16.161.37");
            configs.Add("DF_DOMAIN", "server161-35");
            configs.Add("DF_USER", "public");
            configs.Add("DF_PASSWORD", "public");
            configs.Add("DF_DBNAME", "db_importation");
            configs.Add("DF_DBUSER", "db_fad_junie");
            configs.Add("DF_DBPASS", "pass");
            configs.Add("DF_SELLOCCODE", "DF");

            //Dressing Plant Config
            configs.Add("DP_DATA", @"\\172.16.20.1\fad_ubay\Database\database\");
            configs.Add("DP_CNVPICT", @"\\172.16.20.1\fad_ubay\Database\CNVPICT\");
            configs.Add("DP_MASTERFILE", @"\\172.16.161.35\fad_stores\FAD_MASTERFILE\masterfile\");
            configs.Add("DP_SERVER", "172.16.161.37");
            configs.Add("DP_DOMAIN", "server161-35");
            configs.Add("DP_USER", "public");
            configs.Add("DP_PASSWORD", "public");
            configs.Add("DP_DBNAME", "db_importation");
            configs.Add("DP_DBUSER", "db_fad_junie");
            configs.Add("DP_DBPASS", "pass");
            configs.Add("DP_SELLOCCODE", "DP");

            //Feed Mill Config
            configs.Add("FM_DATA", @"\\172.16.20.1\fad_ubay\Database\database\");
            configs.Add("FM_CNVPICT", @"\\172.16.20.1\fad_ubay\Database\CNVPICT\");
            configs.Add("FM_MASTERFILE", @"\\172.16.161.35\fad_stores\FAD_MASTERFILE\masterfile\");
            configs.Add("FM_SERVER", "172.16.161.37");
            configs.Add("FM_DOMAIN", "server161-35");
            configs.Add("FM_USER", "public");
            configs.Add("FM_PASSWORD", "public");
            configs.Add("FM_DBNAME", "db_importation");
            configs.Add("FM_DBUSER", "db_fad_junie");
            configs.Add("FM_DBPASS", "pass");
            configs.Add("FM_SELLOCCODE", "FM");

            //MFI Cortes Poultry Config
            configs.Add("MFICP_DATA", @"\\172.16.192.51\cdc_fad\FAD_POULTRY\database\");
            configs.Add("MFICP_CNVPICT", @"\\172.16.192.51\cdc_fad\FAD_POULTRY\CNVPICT\");
            configs.Add("MFICP_MASTERFILE", @"\\172.16.161.35\fad_stores\FAD_MASTERFILE\masterfile\");
            configs.Add("MFICP_SERVER", "172.16.161.37");
            configs.Add("MFICP_DOMAIN", "");
            configs.Add("MFICP_USER", "public");
            configs.Add("MFICP_PASSWORD", "public123");
            configs.Add("MFICP_DBNAME", "db_importation");
            configs.Add("MFICP_DBUSER", "db_fad_junie");
            configs.Add("MFICP_DBPASS", "pass");
            configs.Add("MFICP_SELLOCCODE", "PLTRY");

            //Grow Out Config
            configs.Add("GO_DATA", @"\\172.16.20.1\fad_ubay\Database\database\");
            configs.Add("GO_CNVPICT", @"\\172.16.20.1\fad_ubay\Database\CNVPICT\");
            configs.Add("GO_MASTERFILE", @"\\172.16.161.35\fad_stores\FAD_MASTERFILE\masterfile\");
            configs.Add("GO_SERVER", "172.16.161.37");
            configs.Add("GO_DOMAIN", "server161-35");
            configs.Add("GO_USER", "public");
            configs.Add("GO_PASSWORD", "public");
            configs.Add("GO_DBNAME", "db_importation");
            configs.Add("GO_DBUSER", "db_fad_junie");
            configs.Add("GO_DBPASS", "pass");
            configs.Add("GO_SELLOCCODE", "GO");

            //Corporate Head Office Config
            configs.Add("HO_DATA", @"\\172.16.161.35\fad_stores\FAD_STORE_DATABASE\HO_DATA\database\");
            configs.Add("HO_CNVPICT", @"\\172.16.161.35\fad_stores\FAD_STORE_DATABASE\HO_DATA\CNVPICT\");
            configs.Add("HO_MASTERFILE", @"\\172.16.161.35\fad_stores\FAD_MASTERFILE\masterfile\");
            configs.Add("HO_SERVER", "172.16.161.37");
            configs.Add("HO_DOMAIN", "server161-35");
            configs.Add("HO_USER", "public");
            configs.Add("HO_PASSWORD", "public");
            configs.Add("HO_DBNAME", "db_importation");
            configs.Add("HO_DBUSER", "db_fad_junie");
            configs.Add("HO_DBPASS", "pass");
            configs.Add("HO_SELLOCCODE", "HO");

            //Island City Mall Config
            configs.Add("ICM_DATA", @"\\172.16.161.35\fad_stores\FAD_STORE_DATABASE\ICM_DATA\database\");
            configs.Add("ICM_CNVPICT", @"\\172.16.161.35\fad_stores\FAD_STORE_DATABASE\ICM_DATA\CNVPICT\");
            configs.Add("ICM_MASTERFILE", @"\\172.16.161.35\fad_stores\FAD_MASTERFILE\masterfile\");
            configs.Add("ICM_SERVER", "172.16.161.37");
            configs.Add("ICM_DOMAIN", "server161-35");
            configs.Add("ICM_USER", "public");
            configs.Add("ICM_PASSWORD", "public");
            configs.Add("ICM_DBNAME", "db_importation");
            configs.Add("ICM_DBUSER", "db_fad_junie");
            configs.Add("ICM_DBPASS", "pass");
            configs.Add("ICM_SELLOCCODE", "ICM");

            //Plaza Marcela Config
            configs.Add("PM_DATA", @"\\172.16.161.35\fad_stores\FAD_STORE_DATABASE\PM_DATA\database\");
            configs.Add("PM_CNVPICT", @"\\172.16.161.35\fad_stores\FAD_STORE_DATABASE\PM_DATA\CNVPICT\");
            configs.Add("PM_MASTERFILE", @"\\172.16.161.35\fad_stores\FAD_MASTERFILE\masterfile\");
            configs.Add("PM_SERVER", "172.16.161.37");
            configs.Add("PM_DOMAIN", "server161-35");
            configs.Add("PM_USER", "public");
            configs.Add("PM_PASSWORD", "public");
            configs.Add("PM_DBNAME", "db_importation");
            configs.Add("PM_DBUSER", "db_fad_junie");
            configs.Add("PM_DBPASS", "pass");
            configs.Add("PM_SELLOCCODE", "PM");

            //Post Harvest Config
            configs.Add("PH_DATA", @"\\172.16.20.1\fad_ubay\Database\database\");
            configs.Add("PH_CNVPICT", @"\\172.16.20.1\fad_ubay\Database\CNVPICT\");
            configs.Add("PH_MASTERFILE", @"\\172.16.161.35\fad_stores\FAD_MASTERFILE\masterfile\");
            configs.Add("PH_SERVER", "172.16.161.37");
            configs.Add("PH_DOMAIN", "server161-35");
            configs.Add("PH_USER", "public");
            configs.Add("PH_PASSWORD", "public");
            configs.Add("PH_DBNAME", "db_importation");
            configs.Add("PH_DBUSER", "db_fad_junie");
            configs.Add("PH_DBPASS", "pass");
            configs.Add("PH_SELLOCCODE", "PH");

            //Rice Mill Config
            configs.Add("RM_DATA", @"\\172.16.20.1\fad_ubay\Database\database\");
            configs.Add("RM_CNVPICT", @"\\172.16.20.1\fad_ubay\Database\CNVPICT\");
            configs.Add("RM_MASTERFILE", @"\\172.16.161.35\fad_stores\FAD_MASTERFILE\masterfile\");
            configs.Add("RM_SERVER", "172.16.161.37");
            configs.Add("RM_DOMAIN", "");
            configs.Add("RM_USER", "public");
            configs.Add("RM_PASSWORD", "public");
            configs.Add("RM_DBNAME", "db_importation");
            configs.Add("RM_DBUSER", "db_fad_junie");
            configs.Add("RM_DBPASS", "pass");
            configs.Add("RM_SELLOCCODE", "RM");

            //MIC Ubay Config
            configs.Add("MICU_DATA", @"\\172.16.20.1\fad_ubay\Database\database\");
            configs.Add("MICU_CNVPICT", @"\\172.16.20.1\fad_ubay\Database\CNVPICT\");
            configs.Add("MICU_MASTERFILE", @"\\172.16.161.35\fad_stores\FAD_MASTERFILE\masterfile\");
            configs.Add("MICU_SERVER", "172.16.161.37");
            configs.Add("MICU_DOMAIN", "server161-35");
            configs.Add("MICU_USER", "public");
            configs.Add("MICU_PASSWORD", "public");
            configs.Add("MICU_DBNAME", "db_importation");
            configs.Add("MICU_DBUSER", "db_fad_junie");
            configs.Add("MICU_DBPASS", "pass");
            configs.Add("MICU_SELLOCCODE", "MICU");

            //Test Config
            configs.Add("TEST_DATA", @"\\172.16.161.35\fad_stores\FAD_STORE_DATABASE\TEST_DATA\ho_data\database\");
            configs.Add("TEST_CNVPICT", @"\\172.16.161.35\fad_stores\FAD_STORE_DATABASE\TEST_DATA\ho_data\CNVPICT\");
            configs.Add("TEST_MASTERFILE", @"\\172.16.161.35\fad_stores\FAD_MASTERFILE\masterfile\");
            configs.Add("TEST_SERVER", "172.16.43.95");
            configs.Add("TEST_DOMAIN", "server161-35");
            configs.Add("TEST_USER", "public");
            configs.Add("TEST_PASSWORD", "public");
            configs.Add("TEST_DBNAME", "db_importation");
            configs.Add("TEST_DBUSER", "db_fad_junie");
            configs.Add("TEST_DBPASS", "pass");
            configs.Add("TEST_SELLOCCODE", "HO");

                
            //Local Config
            configs.Add("LOCAL_DATA", @"C:\Users\it-programmer\Desktop\local\database\");
            configs.Add("LOCAL_CNVPICT", @"C:\Users\it-programmer\Desktop\local\CNVPICT\");
            configs.Add("LOCAL_MASTERFILE", @"C:\Users\it-programmer\Desktop\local\masterfile\");
            configs.Add("LOCAL_SERVER", "172.16.43.95"); //configs.Add("LOCAL_SERVER", "localhost");
            configs.Add("LOCAL_DOMAIN", "");
            configs.Add("LOCAL_USER", "");
            configs.Add("LOCAL_PASSWORD", "");
            configs.Add("LOCAL_DBNAME", "db_importation");
            configs.Add("LOCAL_DBUSER", "db_fad_junie");
            configs.Add("LOCAL_DBPASS", "pass");
            configs.Add("LOCAL_SELLOCCODE", "HO");
        }

        private static Dictionary<string, string> configs;

        public Dictionary<string, string> Configs { get { return configs; } }
    }

 
}
