#include "addresses.h"


AddressContext create_address_context() {

    uintptr_t base_offset = reinterpret_cast<uintptr_t>(GetModuleHandleA(nullptr));
    uintptr_t scheduler_address = base_offset + 0x6D9250;
    uintptr_t getstate_address = base_offset + 0x367380;

    return AddressContext{
        base_offset,
        scheduler_address,
        getstate_address,
        {},
        nullptr
    };
}
