using Mdo.DB.Entities;
using Mdo.Models.Models;

namespace Mdo.Persistence.Adapters
{
    public class ExpansionAdapter
    {
        private ExpansionModel _expansionModel;
        private ExpansionEntity _expansionEntity;

        public ExpansionAdapter(ExpansionModel expansionModel)
        {
            _expansionModel = expansionModel;
            _expansionEntity = CreateEntity();
        }

        public ExpansionAdapter(ExpansionEntity expansionEntity)
        {
            _expansionEntity = expansionEntity;
            _expansionModel = CreateModel();
        }

        public ExpansionModel ExpansionModel
        {
            get { return _expansionModel; }
        }

        public ExpansionEntity ExpansionEntity
        {
            get { return _expansionEntity; }
        }

        private ExpansionModel CreateModel()
        {
            if (_expansionEntity == null)
            {
                return null;
            }

            var expansionModel = new ExpansionModel(
                _expansionEntity.Name,
                _expansionEntity.ImagePath,
                _expansionEntity.Started);

            return expansionModel;
        }

        private ExpansionEntity CreateEntity()
        {
            if (_expansionModel == null)
            {
                return null;
            }

            var expansionEntity = new ExpansionEntity()
            {
                Name = _expansionModel.Name,
                ImagePath = _expansionModel.ImagePath,
                Started = _expansionModel.Started
            };

            return expansionEntity;
        }
    }
}
