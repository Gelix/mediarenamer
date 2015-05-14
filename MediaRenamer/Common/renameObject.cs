using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MediaRenamer.Series;
using MediaRenamer.Movies;
using System.Windows.Forms;
using System.Threading;

namespace MediaRenamer.Common {
    public class renameObject {
        private String _filename;
        private String _targetName;
        public Boolean copyFile = false;
        public Boolean localRename = false;

        private ProgressBar _progress = null;
        private ToolTip _tip = null;

        public renameObject(String file) {
            _filename = file;
        }

        public ProgressBar progress {
            get {
                return _progress;
            }
            set {
                _progress = value;
            }
        }

        public event RenameDone RenameDone;
        protected virtual void OnRenameDone() {
            RenameDone handler = RenameDone;
            if (handler != null) {
                handler.Invoke(this);
            }
        }

        public void progressHover(object sender, EventArgs e) {
            if (_tip == null) {
                _tip = new ToolTip();
                _tip.SetToolTip(this.progress, (copyFile ? "Copying file" : "Moving file") + "\n" + _filename +
                     "\n to \n" + _targetName);
            }
        }

        public void rename() {
            FileInfo fi = new FileInfo(_filename.Trim());
            if (Episode.validEpisodeFile(fi.Name)) {
                Episode ep = Episode.parseFile(fi.FullName);
                SeriesLocations locations = new SeriesLocations();
                String path = locations.getEpisodePath(ep);
                _targetName = ep.modifiedName();
                if (!localRename && path != null && Directory.Exists(path)) {
                    ep.renameEpisodeAndMove(path, copyFile);
                    locations.addSeriesLocation(ep);
                }
                else {
                    ep.renameEpisode();
                }
            }
            else {
                Movie movie = Movie.parseFile(fi.FullName);
                _targetName = movie.modifiedName();
                if (!localRename && Settings.GetValueAsBool(SettingKeys.MoveMovies)) {
                    String path = Settings.GetValueAsString(SettingKeys.MovieLocation);
                    movie.renameMovieAndMove(path, copyFile);
                }
                else {
                    movie.renameMovie();
                }
            }

            if (_tip != null) {
                _tip.Dispose();
            }

            this.OnRenameDone();
        }
    }
}
