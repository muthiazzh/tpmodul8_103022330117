using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace tpmodul8_103022330117
{
    class CovidConfig
    {
        private string filePath = "covid_config.json";
        public string satuan_suhu { get; set; }
        public int batas_hari_deman { get; set; }
        public string pesan_ditolak { get; set; }
        public string pesan_diterima { get; set; }

        public CovidConfig()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                CovidConfigData? config = JsonSerializer.Deserialize<CovidConfigData>(json);

                if (config != null)
                {
                    satuan_suhu = config.satuan_suhu;
                    batas_hari_deman = config.batas_hari_deman;
                    pesan_ditolak = config.pesan_ditolak;
                    pesan_diterima = config.pesan_diterima;
                }
            }
            else
            {
                satuan_suhu = "celcius";
                batas_hari_deman = 14;
                pesan_ditolak = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
                pesan_diterima = "Anda dipersilahkan untuk masuk ke dalam gedung ini";
                SaveConfig();
            }
        }

        public void UbahSatuan()
        {
            satuan_suhu = satuan_suhu.ToLower() == "celcius" ? "fahrenheit" : "celcius";
            SaveConfig();
        }

        private void SaveConfig()
        {
            var config = new CovidConfigData
            {
                satuan_suhu = this.satuan_suhu,
                batas_hari_deman = this.batas_hari_deman,
                pesan_ditolak = this.pesan_ditolak,
                pesan_diterima = this.pesan_diterima
            };

            string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }

    class CovidConfigData
    {
        public string satuan_suhu { get; set; }
        public int batas_hari_deman { get; set; }
        public string pesan_ditolak { get; set; }
        public string pesan_diterima { get; set; }
    }
}
