//
//  NativePlatform.mm
//  Unity-iPhone
//
//  Created by zhangxd on 14-8-6.
//
//

#include <sys/sysctl.h>
#include <mach/mach.h>


extern "C"
{
    
    unsigned int _MemoryUsage()
    {
        struct task_basic_info info;
        mach_msg_type_number_t size = sizeof(info);
        kern_return_t kerr = task_info(mach_task_self(),
                                       TASK_BASIC_INFO,
                                       (task_info_t)&info,
                                       &size);
        if( kerr == KERN_SUCCESS )
        {
            return info.resident_size;
        }
        else
        {
            return 0;
        }
    }
    
}
