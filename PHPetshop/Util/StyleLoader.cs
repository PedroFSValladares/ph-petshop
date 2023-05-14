using System.Text;

namespace PHPetshop.Util {
    public static class StyleLoader {
        const string stylesPath = @"wwwroot/css/";
        const string href = @"/css/";
        public static string RenderStyleSection(string folderName) {
            StringBuilder stringBuilder = new StringBuilder();
            List<string> files = Directory.EnumerateFiles(stylesPath + folderName).ToList();
            var html = files.Select((element) => {
                string fileName = element.ToString().Substring(element.LastIndexOf('\\') + 1);
                return $"<link rel='stylesheet' href='{href}{folderName}/{fileName}' />\n";
            });
            Console.WriteLine(stringBuilder.ToString());
            stringBuilder.AppendJoin(null, html);
            return stringBuilder.ToString();
        }
    }
}
