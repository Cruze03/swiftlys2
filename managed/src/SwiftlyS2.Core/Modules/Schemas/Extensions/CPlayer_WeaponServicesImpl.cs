using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CPlayer_WeaponServicesImpl
{
    public void DropWeapon( CBasePlayerWeapon weapon )
    {
        NativeBinding.ThrowIfNonMainThread();
        GameFunctions.CCSPlayer_WeaponServices_DropWeapon(Address, weapon.Address);
    }

    public void RemoveWeapon( CBasePlayerWeapon weapon )
    {
        NativeBinding.ThrowIfNonMainThread();
        GameFunctions.CCSPlayer_WeaponServices_DropWeapon(Address, weapon.Address);
        weapon.Despawn();
    }

    public void SelectWeapon( CBasePlayerWeapon weapon )
    {
        NativeBinding.ThrowIfNonMainThread();
        GameFunctions.CCSPlayer_WeaponServices_SelectWeapon(Address, weapon.Address);
    }

    public void DropWeaponBySlot( gear_slot_t slot )
    {
        NativeBinding.ThrowIfNonMainThread();
        MyWeapons.ToList().ForEach(weapon =>
        {
            if (weapon.Value?.As<CCSWeaponBase>().WeaponBaseVData.GearSlot == slot)
            {
                DropWeapon(weapon.Value);
            }
        });
    }

    public void RemoveWeaponBySlot( gear_slot_t slot )
    {
        NativeBinding.ThrowIfNonMainThread();
        MyWeapons.ToList().ForEach(weapon =>
        {
            if (weapon.Value?.As<CCSWeaponBase>().WeaponBaseVData.GearSlot == slot)
            {
                RemoveWeapon(weapon.Value);
            }
        });
    }

    public void SelectWeaponBySlot( gear_slot_t slot )
    {
        NativeBinding.ThrowIfNonMainThread();
        MyWeapons.ToList().ForEach(weapon =>
        {
            if (weapon.Value?.As<CCSWeaponBase>().WeaponBaseVData.GearSlot == slot)
            {
                SelectWeapon(weapon.Value);
                return;
            }
        });
    }

    public void DropWeaponByDesignerName( string designerName )
    {
        NativeBinding.ThrowIfNonMainThread();
        MyWeapons.ToList().ForEach(weapon =>
        {
            if (weapon.Value?.Entity?.DesignerName == designerName)
            {
                DropWeapon(weapon.Value);
            }
        });
    }

    public void RemoveWeaponByDesignerName( string designerName )
    {
        NativeBinding.ThrowIfNonMainThread();
        MyWeapons.ToList().ForEach(weapon =>
        {
            if (weapon.Value?.Entity?.DesignerName == designerName)
            {
                RemoveWeapon(weapon.Value);
            }
        });
    }

    public void SelectWeaponByDesignerName( string designerName )
    {
        NativeBinding.ThrowIfNonMainThread();
        MyWeapons.ToList().ForEach(weapon =>
        {
            if (weapon.Value?.Entity?.DesignerName == designerName)
            {
                SelectWeapon(weapon.Value);
            }
        });
    }

    public void DropWeaponByClass<T>() where T : class, ISchemaClass<T>
    {
        NativeBinding.ThrowIfNonMainThread();
        var name = T.ClassName;
        if (name == null)
        {
            throw new ArgumentException(
                $"Can't drop weapon with class {typeof(T).Name}, which doesn't have a designer name.");
        }

        DropWeaponByDesignerName(name);
    }

    public void RemoveWeaponByClass<T>() where T : class, ISchemaClass<T>
    {
        NativeBinding.ThrowIfNonMainThread();
        var name = T.ClassName;
        if (name == null)
        {
            throw new ArgumentException(
                $"Can't drop weapon with class {typeof(T).Name}, which doesn't have a designer name.");
        }

        RemoveWeaponByDesignerName(name);
    }

    public void SelectWeaponByClass<T>() where T : class, ISchemaClass<T>
    {
        NativeBinding.ThrowIfNonMainThread();
        var name = T.ClassName;
        if (name == null)
        {
            throw new ArgumentException(
                $"Can't drop weapon with class {typeof(T).Name}, which doesn't have a designer name.");
        }

        SelectWeaponByDesignerName(name);
    }

    public Task DropWeaponAsync( CBasePlayerWeapon weapon )
    {
        return SchedulerManager.QueueOrNow(() => DropWeapon(weapon));
    }

    public Task RemoveWeaponAsync( CBasePlayerWeapon weapon )
    {
        return SchedulerManager.QueueOrNow(() => RemoveWeapon(weapon));
    }

    public Task SelectWeaponAsync( CBasePlayerWeapon weapon )
    {
        return SchedulerManager.QueueOrNow(() => SelectWeapon(weapon));
    }

    public Task DropWeaponBySlotAsync( gear_slot_t slot )
    {
        return SchedulerManager.QueueOrNow(() => DropWeaponBySlot(slot));
    }

    public Task RemoveWeaponBySlotAsync( gear_slot_t slot )
    {
        return SchedulerManager.QueueOrNow(() => RemoveWeaponBySlot(slot));
    }

    public Task SelectWeaponBySlotAsync( gear_slot_t slot )
    {
        return SchedulerManager.QueueOrNow(() => SelectWeaponBySlot(slot));
    }

    public Task DropWeaponByDesignerNameAsync( string designerName )
    {
        return SchedulerManager.QueueOrNow(() => DropWeaponByDesignerName(designerName));
    }

    public Task RemoveWeaponByDesignerNameAsync( string designerName )
    {
        return SchedulerManager.QueueOrNow(() => RemoveWeaponByDesignerName(designerName));
    }

    public Task SelectWeaponByDesignerNameAsync( string designerName )
    {
        return SchedulerManager.QueueOrNow(() => SelectWeaponByDesignerName(designerName));
    }

    public Task DropWeaponByClassAsync<T>() where T : class, ISchemaClass<T>
    {
        return SchedulerManager.QueueOrNow(() => DropWeaponByClass<T>());
    }

    public Task RemoveWeaponByClassAsync<T>() where T : class, ISchemaClass<T>
    {
        return SchedulerManager.QueueOrNow(() => RemoveWeaponByClass<T>());
    }

    public Task SelectWeaponByClassAsync<T>() where T : class, ISchemaClass<T>
    {
        return SchedulerManager.QueueOrNow(() => SelectWeaponByClass<T>());
    }

    public IEnumerable<CBasePlayerWeapon> MyValidWeapons => MyWeapons.ToList()
        .Where(w => w.IsValid && w.Value != null && w.Value.IsValid).Select(w => w.Value!);
}