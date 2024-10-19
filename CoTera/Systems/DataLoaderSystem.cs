using URAPI;
using Microsoft.Maui.Controls;
using Syncfusion.Maui.PdfToImageConverter;

namespace CoTera.Systems
{
    internal static class DataLoaderSystem
    {
        public static int SelectedCollageIndex;
        public static int SelectedMajorIndex;
        public static int SelectedScheduleIndex;

        public static readonly string PdfPath = Path.Combine(FileSystem.CacheDirectory + "\\saved.png");
        public static readonly string ConfFilePath = Path.Combine(FileSystem.CacheDirectory + "\\data.conf");
        public static Stream PDFStream;

        public static async void Initialize()
        {
            SelectedCollageIndex = 0;
            SelectedMajorIndex = 0;
            SelectedScheduleIndex = 0;

            await LoadData();
        }

        public static async Task SaveData()
        {
            var b = await OptionsPage.Instance.Schedules[SelectedScheduleIndex].GetScheduleMemoryStream();
            string saveDataText = $"CollageIndex={SelectedCollageIndex}\n" +
                                  $"MajorIndex={SelectedMajorIndex}\n" +
                                  $"ScheduleIndex={SelectedScheduleIndex}";
            FileStream fs = new(PdfPath, FileMode.OpenOrCreate);
            //save
            Task confSaveTask = File.WriteAllTextAsync(ConfFilePath, saveDataText);

            PdfToImageConverter imageConverter = new PdfToImageConverter();
            imageConverter.Load(b);
            Stream s = imageConverter.Convert(0);
            imageConverter.Dispose();
            s.CopyTo(fs);

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
            string msg = (File.Exists(PdfPath)).ToString() + " /// " + PdfPath;
            AppControllerSystem.Alert("T", msg, "C");
        }

    }
}