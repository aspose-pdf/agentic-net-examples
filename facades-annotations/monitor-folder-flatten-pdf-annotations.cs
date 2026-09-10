using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfFlattenerService
{
    private readonly string _watchFolder;
    private readonly string _outputFolder;
    private FileSystemWatcher _watcher;

    public PdfFlattenerService(string watchFolder, string outputFolder)
    {
        _watchFolder = watchFolder;
        _outputFolder = outputFolder;
    }

    public void Start()
    {
        // Ensure the output directory exists
        Directory.CreateDirectory(_outputFolder);

        // Watch for new PDF files
        _watcher = new FileSystemWatcher(_watchFolder, "*.pdf");
        _watcher.Created += OnCreated;
        _watcher.EnableRaisingEvents = true;

        Console.WriteLine($"Monitoring folder: {_watchFolder}");
    }

    private void OnCreated(object sender, FileSystemEventArgs e)
    {
        try
        {
            // Wait until the file is ready for reading
            for (int i = 0; i < 5; i++)
            {
                if (IsFileReady(e.FullPath))
                    break;
                System.Threading.Thread.Sleep(500);
            }

            ProcessPdf(e.FullPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing file '{e.Name}': {ex.Message}");
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

    private void ProcessPdf(string inputPath)
    {
        string fileName = Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = Path.Combine(_outputFolder, $"{fileName}_flattened.pdf");

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Flatten annotations using PdfAnnotationEditor
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);
                editor.FlatteningAnnotations(); // flatten all annotations
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }

    public void Stop()
    {
        if (_watcher != null)
        {
            _watcher.EnableRaisingEvents = false;
            _watcher.Dispose();
        }
    }

    static void Main(string[] args)
    {
        // Expect two arguments: watch folder and output folder
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PdfFlattenerService <watchFolder> <outputFolder>");
            return;
        }

        PdfFlattenerService service = new PdfFlattenerService(args[0], args[1]);
        service.Start();

        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();

        service.Stop();
    }
}