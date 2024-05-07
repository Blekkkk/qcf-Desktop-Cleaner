using System;
using System.IO;
using System.Linq;
using System.Security.Principal;

namespace DesktopOrganizer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!IsAdministrator())
            {
                Console.WriteLine("NO");
                return;
            }

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string allFolderPath = Path.Combine(desktopPath, "all");
            Directory.CreateDirectory(allFolderPath);

            CreateSubfolderAndOrganizeFiles(desktopPath, allFolderPath, "Shortcuts", new string[] { ".lnk", ".url", ".webloc", ".webpnp", ".website" });
            CreateSubfolderAndOrganizeFiles(desktopPath, allFolderPath, "Pictures", new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".tif", ".psd", ".eps", ".ai", ".indd", ".raw", ".cr2", ".nef", ".raf", ".dng", ".jfif", ".jpe", ".jp2", ".j2k", ".jpf", ".jpx", ".jpm", ".mj2", ".svg", ".webp", ".heic", ".heif", ".avif", ".arw", ".cr3", ".orf", ".rw2", ".rwl", ".srw" });
            CreateSubfolderAndOrganizeFiles(desktopPath, allFolderPath, "Applications", new string[] { ".exe", ".msi", ".appx", ".appxbundle", ".msix", ".msixbundle", ".app", ".ipa", ".apk", ".jar", ".war", ".ear", ".run", ".bin", ".deb", ".rpm", ".pkg", ".dmg", ".vb", ".vbs", ".js", ".jsx", ".php", ".py", ".pyc", ".pyo", ".pyz", ".pyw", ".rb", ".pl", ".sh", ".bat", ".cmd", ".com", ".cpl", ".dll", ".drv", ".efi", ".fon", ".fxr", ".hlp", ".ini", ".ins", ".isp", ".its", ".jse", ".lib", ".lnk", ".msc", ".msi", ".msm", ".msp", ".mst", ".nls", ".ocx", ".olb", ".pcd", ".pif", ".pnf", ".prf", ".reg", ".scf", ".scr", ".sct", ".shb", ".sys", ".tsp", ".url", ".vbe", ".vbx", ".ws", ".wsf", ".wsh" });
            CreateSubfolderAndOrganizeFiles(desktopPath, allFolderPath, "Music", new string[] { ".mp3", ".wav", ".aac", ".flac", ".ogg", ".wma", ".m4a", ".aiff", ".au", ".ra", ".ram", ".mid", ".midi", ".rmi", ".it", ".xm", ".mod", ".s3m", ".ape", ".dsf", ".dff", ".opus", ".spx", ".tta" });
            CreateSubfolderAndOrganizeFiles(desktopPath, allFolderPath, "Video", new string[] { ".mp4", ".mov", ".avi", ".mkv", ".flv", ".wmv", ".webm", ".m4v", ".3gp", ".3g2", ".f4v", ".asf", ".vob", ".ogv", ".ogg", ".dv", ".qt", ".yuv", ".rm", ".rmvb", ".viv", ".drc", ".gifv", ".mng", ".m4p", ".m4r", ".f4p", ".f4a", ".f4b" });
            CreateSubfolderAndOrganizeFiles(desktopPath, allFolderPath, "Archives", new string[] { ".zip", ".rar", ".7z", ".tar", ".gz", ".tgz", ".bz2", ".xz", ".lzma", ".cab", ".arj", ".lzh", ".chm", ".split", ".sitx", ".sqx", ".uc2", ".wim", ".esd", ".wsz", ".xar", ".alz", ".egg", ".War", ".Ear", ".zia", ".bzip2", ".tbz2", ".lzx", ".par", ".rev" });
            CreateSubfolderAndOrganizeFiles(desktopPath, allFolderPath, "Documents", new string[] { ".doc", ".docx", ".odt", ".rtf", ".txt", ".pdf", ".xls", ".xlsx", ".ods", ".ppt", ".pptx", ".odp" });

            Console.WriteLine("DONE");
        }

        private static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static void CreateSubfolderAndOrganizeFiles(string desktopPath, string allFolderPath, string subfolderName, string[] fileExtensions)
        {
            string subfolderPath = Path.Combine(allFolderPath, subfolderName);
            Directory.CreateDirectory(subfolderPath);

            var files = Directory.GetFiles(desktopPath).Where(f => fileExtensions.Contains(Path.GetExtension(f).ToLower()));
            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                File.Move(file, Path.Combine(subfolderPath, fileName));
            }
        }
    }
}