namespace Enemies
{
    public interface IShootable
    {
        void Shot(Projectile projectile, Shooter source);
    }
}