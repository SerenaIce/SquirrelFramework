namespace SquirrelFramework.Configurations
{
    #region using directives

    using System.IO;
    using System.Xml.Serialization;

    #endregion using directives

    /// <summary>
    /// 可以通过定义 xsd 文件，并通过 vs cmd 的 xsd 命令，将 xsd 文件转换为 C# 类
    /// 随后可以通过本类对这个 XML 的映射 Model 进行读写
    /// </summary>
    /// <typeparam name="TXmlModel">有 xsd 生成的 XML model 类</typeparam>
    public class XmlManager<TXmlModel>
    {
        public string XmlFilePath { get; }
        public bool IsEnableFileWatcher { get; private set; }
        private XmlSerializer serializer;
        private FileSystemWatcher fileWatcher;

        public XmlManager(string xmlFilePath, bool isEnableFileWatcher = false)
        {
            this.XmlFilePath = xmlFilePath;
            this.serializer = new XmlSerializer(typeof(TXmlModel));
            this.IsEnableFileWatcher = isEnableFileWatcher;
        }

        public TXmlModel GetModel()
        {
            using (var stream = new FileStream(this.XmlFilePath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                return (TXmlModel)this.serializer.Deserialize(stream);
            }
        }

        public void SaveModel(TXmlModel model)
        {
            using (var stream = new FileStream(this.XmlFilePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                this.serializer.Serialize(stream, model);
            }
        }

        public void EnableFileWatcher(FileSystemEventHandler fileSystemEventHandler)
        {
            if (this.fileWatcher != null)
            {
                this.fileWatcher.Dispose();
            }
            this.IsEnableFileWatcher = true;
            this.fileWatcher = new FileSystemWatcher
            {
                Path = Path.GetDirectoryName(this.XmlFilePath),
                IncludeSubdirectories = false,
                Filter = Path.GetFileName(this.XmlFilePath),
                //Filter = "*.config"
                EnableRaisingEvents = this.IsEnableFileWatcher
            };
            this.fileWatcher.Changed += fileSystemEventHandler;
        }

        public void DisableFileWatcher()
        {
            if (this.fileWatcher != null)
            {
                this.fileWatcher.Dispose();
            }
            this.IsEnableFileWatcher = false;
        }
    }
}