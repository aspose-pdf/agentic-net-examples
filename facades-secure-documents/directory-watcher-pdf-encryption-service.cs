using System;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // required per specification

class PdfEncryptionService
{
    private readonly string watchPath;
    private readonly string archivePath;
    private readonly string userPassword;
    private readonly string ownerPassword;
    private readonly FileSystemWatcher watcher;

    public PdfEncryptionService(string watchPath, string archivePath, string userPassword, string ownerPassword)
    {
        this.watchPath = watchPath;
        this.archivePath = archivePath;
        this.userPassword = userPassword;
        this.ownerPassword = ownerPassword;

        Directory.CreateDirectory(this.watchPath);
        Directory.CreateDirectory(this.archivePath);

        watcher = new FileSystemWatcher(this.watchPath, "*.pdf");
        watcher.Created += OnCreated;
        watcher.IncludeSubdirectories = false;
        watcher.EnableRaisingEvents = true;
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        try
        {
            // Ensure the file is fully written before processing
            for (int i = 0; i < 5 && !IsFileReady(e.FullPath); i++)
                Thread.Sleep(500);

            if (!IsFileReady(e.FullPath))
                return;

            string fileName = Path.GetFileName(e.FullPath);
            string destPath = Path.Combine(archivePath, fileName);

            // Encrypt the PDF and save to the archive folder
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(e.FullPath))
            {
                Aspose.Pdf.Permissions perms = Aspose.Pdf.Permissions.PrintDocument |
                                               Aspose.Pdf.Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, Aspose.Pdf.CryptoAlgorithm.AESx256);
                doc.Save(destPath);
            }

            // Delete the original file after successful encryption
            File.Delete(e.FullPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing '{e.FullPath}': {ex.Message}");
        }
    }

    private bool IsFileReady(string path)
    {
        try
        {
            using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                return stream.Length > 0;
            }
        }
        catch
        {
            return false;
        }
    }

    public void Start()
    {
        Console.WriteLine($"Monitoring '{watchPath}' for new PDF files...");
    }

    public void Stop()
    {
        watcher.EnableRaisingEvents = false;
        watcher.Dispose();
    }

    static void Main(string[] args)
    {
        // Expected arguments:
        // args[0] - directory to watch
        // args[1] - archive directory
        // args[2] - user password for encryption
        // args[3] - owner password for encryption
        if (args.Length < 4)
        {
            Console.WriteLine("Usage: PdfEncryptionService <watchFolder> <archiveFolder> <userPassword> <ownerPassword>");
            return;
        }

        var service = new PdfEncryptionService(args[0], args[1], args[2], args[3]);
        service.Start();

        Console.WriteLine("Press Enter to stop the service...");
        Console.ReadLine();

        service.Stop();
    }
}