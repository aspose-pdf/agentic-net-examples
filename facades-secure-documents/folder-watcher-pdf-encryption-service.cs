using System;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;

class PdfEncryptionService
{
    private readonly string _watchFolder;
    private readonly string _archiveFolder;
    private readonly string _userPassword;
    private readonly string _ownerPassword;
    private readonly FileSystemWatcher _watcher;

    public PdfEncryptionService(string watchFolder, string archiveFolder, string userPassword, string ownerPassword)
    {
        _watchFolder = watchFolder;
        _archiveFolder = archiveFolder;
        _userPassword = userPassword;
        _ownerPassword = ownerPassword;

        // Ensure archive folder exists
        Directory.CreateDirectory(_archiveFolder);

        _watcher = new FileSystemWatcher(_watchFolder, "*.pdf")
        {
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.CreationTime,
            EnableRaisingEvents = false,
            IncludeSubdirectories = false
        };
        _watcher.Created += OnCreated;
    }

    public void Start()
    {
        _watcher.EnableRaisingEvents = true;
        Console.WriteLine($"Monitoring folder: {_watchFolder}");
    }

    public void Stop()
    {
        _watcher.EnableRaisingEvents = false;
        _watcher.Created -= OnCreated;
        _watcher.Dispose();
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        // Run processing on a separate thread to avoid blocking the watcher
        ThreadPool.QueueUserWorkItem(_ => ProcessFile(e.FullPath));
    }

    private void ProcessFile(string sourcePath)
    {
        // Wait until the file is fully written
        const int maxAttempts = 10;
        int attempt = 0;
        while (attempt < maxAttempts)
        {
            try
            {
                using (FileStream fs = File.Open(sourcePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    // If we can open the file exclusively, it is ready
                    break;
                }
            }
            catch (IOException)
            {
                Thread.Sleep(500);
                attempt++;
            }
        }

        string fileName = Path.GetFileName(sourcePath);
        string encryptedPath = Path.Combine(_archiveFolder, fileName);

        try
        {
            // Initialize PdfFileSecurity with input and output paths
            using (PdfFileSecurity security = new PdfFileSecurity(sourcePath, encryptedPath))
            {
                // Encrypt with user/owner passwords, allow printing, 256‑bit AES
                security.EncryptFile(
                    _userPassword,
                    _ownerPassword,
                    DocumentPrivilege.Print,
                    KeySize.x256);
            }

            // Optionally delete the original unencrypted file
            File.Delete(sourcePath);
            Console.WriteLine($"Encrypted and archived: {encryptedPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing '{sourcePath}': {ex.Message}");
        }
    }

    // Example entry point
    static void Main(string[] args)
    {
        if (args.Length < 4)
        {
            Console.WriteLine("Usage: PdfEncryptionService <watchFolder> <archiveFolder> <userPassword> <ownerPassword>");
            return;
        }

        string watchFolder = args[0];
        string archiveFolder = args[1];
        string userPassword = args[2];
        string ownerPassword = args[3];

        var service = new PdfEncryptionService(watchFolder, archiveFolder, userPassword, ownerPassword);
        service.Start();

        Console.WriteLine("Press ENTER to stop...");
        Console.ReadLine();

        service.Stop();
    }
}