using URAPI;

namespace CoTera.Systems
{
    internal static class DataLoaderSystem
    {
        public static int SelectedCollageIndex;
        public static int SelectedMajorIndex;
        public static int SelectedScheduleIndex;

        public static readonly string PdfFilePath = Path.Combine(FileSystem.CacheDirectory + "/savedpdf.pdf");
        public static readonly string ConfFilePath = Path.Combine(FileSystem.CacheDirectory + "/data.conf");

        public static async void Initialize()
        {
            SelectedCollageIndex = 0;
            SelectedMajorIndex = 0;
            SelectedScheduleIndex = 0;

            await LoadData();
        }

        public static async Task SaveData()
        {
            //save pdf
            byte[] bytes = await OptionsPage.Instance.Schedules[SelectedScheduleIndex].GetPDFBytes();
            Task pdfSaveTask = File.WriteAllBytesAsync(PdfFilePath, bytes);
            string saveDataText = $"CollageIndex={SelectedCollageIndex}\n" +
                                  $"MajorIndex={SelectedMajorIndex}\n" +
                                  $"ScheduleIndex={SelectedScheduleIndex}";
            //save
            Task confSaveTask = File.WriteAllTextAsync(ConfFilePath, saveDataText);
            await pdfSaveTask;
            await confSaveTask;

        }

        public static async Task LoadData()
        {
            if (!File.Exists(ConfFilePath))
                return;
            string[] lines = await File.ReadAllLinesAsync(ConfFilePath);
            SelectedCollageIndex = int.Parse(lines[0].Split("=")[1]);
            SelectedMajorIndex = int.Parse(lines[1].Split("=")[1]);
            SelectedScheduleIndex = int.Parse(lines[2].Split("=")[1]);
        }

        public static async Task RefreshData()
        {
            if (!File.Exists(PdfFilePath))
                return;
            List<Collage> collages = await Client.GetCollages();
            List<Major> majors = await collages[SelectedCollageIndex].GetMajors();
            List<Schedule> schedules = await majors[SelectedMajorIndex].GetSchedules();

            byte[] bytes = await schedules[SelectedScheduleIndex].GetPDFBytes();
            Task pdfSaveTask = File.WriteAllBytesAsync(PdfFilePath, bytes);
            await pdfSaveTask;
        }

    }
}