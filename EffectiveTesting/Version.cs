using System;

namespace EffectiveTesting
{
    public class Version
    {
        private readonly int _major;
        private readonly int _minor;
        private readonly int _build;
        private readonly int _revision;

        public Version(string version)
        {
            var versionNumbers = version.Split('.');

            _major = Convert.ToInt32(versionNumbers[0]);
            _minor = Convert.ToInt32(versionNumbers[1]);
            _build = Convert.ToInt32(versionNumbers[2]);
            _revision = Convert.ToInt32(versionNumbers[3]);
        }

        public int Revision
        {
            get { return _revision; }
        }

        public int Build
        {
            get { return _build; }
        }

        public int Minor
        {
            get { return _minor; }
        }

        public int Major
        {
            get { return _major; }
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}.{3}", Major, Minor, Build, Revision);
        }
    }
}
