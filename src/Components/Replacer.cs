using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace NetFocus.UtilityTool.CodeGenerator.Commands.Components
{
    public class Replacer
    {
        private string key = null;
        private string value = null;
        private string[] targetFiles = null;

        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        public string[] TargetFiles
        {
            get { return targetFiles; }
            set { targetFiles = value; }
        }
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public void Replace()
        {
            //对获取到的所有文件进行替换
            StreamReader streamReader = null;
            StreamWriter streamWriter = null;
            for (int i = 0; i < targetFiles.Length; i++)
            {
                File.SetAttributes(targetFiles[i], FileAttributes.Normal);

                streamReader = new StreamReader(TargetFiles[i], Encoding.UTF8, true);
                string content = streamReader.ReadToEnd();
                streamReader.Close();
                streamReader.Dispose();

                if (Key != null)
                {
                    content = content.Replace(string.Format("<%={0}%>", Key), Value);
                }
                streamWriter = new StreamWriter(TargetFiles[i], false, Encoding.UTF8);
                streamWriter.Write(content);
                streamWriter.Close();
                streamWriter.Dispose();
            }
        }

        public void Replace(string key)
        {
            //对获取到的所有文件进行替换
            StreamReader streamReader = null;
            StreamWriter streamWriter = null;
            for (int i = 0; i < targetFiles.Length; i++)
            {
                if (!string.IsNullOrEmpty(targetFiles[i]) && File.Exists(targetFiles[i]))
                {
                    File.SetAttributes(targetFiles[i], FileAttributes.Normal);

                    streamReader = new StreamReader(TargetFiles[i], Encoding.UTF8, true);
                    string content = streamReader.ReadToEnd();
                    streamReader.Close();
                    streamReader.Dispose();

                    if (key != null)
                    {
                        content = content.Replace(key, Value);
                    }
                    streamWriter = new StreamWriter(TargetFiles[i], false, Encoding.UTF8);
                    streamWriter.Write(content);
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
        }

    }
}
