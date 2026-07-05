using System;
using System.IO;
using System.Threading;
using Aspose.Pdf.Facades;

class PdfAnnotationFlattenerService
{
    private readonly string _watchFolder;
    private readonly string _outputFolder;
    private readonly FileSystemWatcher _watcher;

    public PdfAnnotationFlattenerService(string watchFolder, string outputFolder)
    {
        _watchFolder = watchFolder;
        _outputFolder = outputFolder;

        // Ensure output folder exists
        Directory.CreateDirectory(_outputFolder);

        _watcher = new FileSystemWatcher(_watchFolder, "*.pdf")
        {
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.CreationTime | NotifyFilters.Size,
            EnableRaisingEvents = false,
            IncludeSubdirectories = false
        };

        _watcher.Created += OnCreated;
        _watcher.Renamed += OnRenamed;
    }

    public void Start()
    {
        _watcher.EnableRaisingEvents = true;
        Console.WriteLine($"Monitoring folder: {_watchFolder}");
    }

    public void Stop()
    {
        _watcher.EnableRaisingEvents = false;
        _watcher.Dispose();
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        ProcessFile(e.FullPath);
    }

    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        // Handle cases where a file is moved into the folder with a .pdf extension
        if (Path.GetExtension(e.FullPath).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
        {
            ProcessFile(e.FullPath);
        }
    }

    private void ProcessFile(string filePath)
    {
        // Run processing on a separate thread to avoid blocking the watcher
        ThreadPool.QueueUserWorkItem(_ =>
        {
            try
            {
                // Wait until the file is ready for reading (some apps lock the file briefly)
                const int maxAttempts = 10;
                int attempts = 0;
                while (attempts < maxAttempts)
                {
                    try
                    {
                        using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                        {
                            // File is accessible; break out of the loop
                            break;
                        }
                    }
                    catch (IOException)
                    {
                        attempts++;
                        Thread.Sleep(500); // wait half a second before retry
                    }
                }

                // Determine output path (same name, placed in output folder)
                string outputPath = Path.Combine(_outputFolder, Path.GetFileName(filePath));

                // Use PdfAnnotationEditor to flatten annotations
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(filePath);
                    editor.FlatteningAnnotations(); // flatten all annotations
                    editor.Save(outputPath);
                }

                Console.WriteLine($"Flattened annotations: '{filePath}' -> '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        });
    }

    static void Main(string[] args)
    {
        // Example usage:
        // args[0] = folder to watch, args[1] = folder to place flattened PDFs
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PdfAnnotationFlattenerService <watchFolder> <outputFolder>");
            return;
        }

        string watchFolder = args[0];
        string outputFolder = args[1];

        if (!Directory.Exists(watchFolder))
        {
            Console.Error.WriteLine($"Watch folder does not exist: {watchFolder}");
            return;
        }

        var service = new PdfAnnotationFlattenerService(watchFolder, outputFolder);
        service.Start();

        Console.WriteLine("Press ENTER to stop the service...");
        Console.ReadLine();

        service.Stop();
    }
}