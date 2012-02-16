using System;

namespace EffectiveTesting
{
    public class Version
    {
        private readonly int _major;
        private readonly int _minor;
        private readonly int _build;
        private readonly int _revision;

        public Version(int major, int minor, int build, int revision)
        {
            _major = major;
            _minor = minor;
            _build = build;
            _revision = revision;
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
            return string.Format("Version = '{0}.{1}.{2}.{3}'", Major, Minor, Build, Revision);
        }

        public ShortVersion ToShortVersion()
        {
            return new ShortVersion {Major = Major, Minor = Minor};
        }


        public static Version Parse(string version)
        {
            var versionNumbers = version.Split('.');

            var major = Convert.ToInt32(versionNumbers[0]);
            var minor = Convert.ToInt32(versionNumbers[1]);
            var build = Convert.ToInt32(versionNumbers[2]);
            var revision = Convert.ToInt32(versionNumbers[3]);

            return new Version(major, minor, build, revision);
        }


        public static Version ParseWithError(string version)
        {
            var versionNumbers = version.Split('.');

            var major = Convert.ToInt32(versionNumbers[0]);
            var minor = Convert.ToInt32(versionNumbers[1]);
            var build = Convert.ToInt32(versionNumbers[3]); //error
            var revision = Convert.ToInt32(versionNumbers[2]); //error

            return new Version(major, minor, build, revision);
        }
    }
}
