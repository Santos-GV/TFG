﻿using WpfAppTFG.Model.DAOs;

namespace WpfAppTFG.Model.Respository
{
    /// <summary>
    /// Implementación de un Repositorio de <see cref="Post"/>
    /// </summary>
    public class PostRepository : Repository<Post>
    {

        public PostRepository() : base(new PostDAO())
        {
        }
    }
}
